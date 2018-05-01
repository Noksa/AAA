using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using ImlFieldDecorator = AT_Core_Specflow.Decorators.ImlFieldDecorator;

namespace AT_Core_Specflow.Core
{
    public abstract class BasePage
    {
        protected ThreadLocal<bool> isUsedBlock = new ThreadLocal<bool>();
        private object _usedBlock;
        protected bool IsUsedBlock
        {
            get
            {
                if (!isUsedBlock.IsValueCreated) isUsedBlock.Value = false;
                return isUsedBlock.Value;
            }
            set => isUsedBlock.Value = value;
        }

        protected BasePage()
        {
            PageManager.PageContext.Elements.Clear();
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ImlFieldDecorator());
        }

        public void ExecuteMethodByTitle(string actionTitle, params object[] parameters)
        {
            var method = FindMethodByActionTitle(actionTitle, parameters);
            method.Invoke(this, parameters);
        }
        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle, params object[] parameters)
        {
            var block = GetElementByTitle(blockName);
            var method = FindMethodByActionTitleInBlock(blockName, actionTitle, parameters);
            if (method != null) method.Invoke(block, parameters);
            IsUsedBlock = true;
            _usedBlock = block;
            method = FindMethodByActionTitle(actionTitle, parameters);
            try
            {
                method.Invoke(this, parameters);
            }
            finally
            {
                IsUsedBlock = false;
            }
        }

        private MethodInfo FindMethodByActionTitle(string actionTitle, object[] parameters)
        {
            // Ищем метод на дочерней странице. Если там его нет - ищем метод в базовом классе.
            var searchedMethod = GetType().GetMethods().FirstOrDefault(method => 
             method.GetCustomAttribute(typeof(ActionTitleAttribute)) is ActionTitleAttribute attr && attr.ActionTitle == actionTitle && CoreFunctions.CheckParamsTypesOfMethod(parameters, method.GetParameters()));
            if (searchedMethod != null) return searchedMethod;
            throw  new NullReferenceException($"Cant find method for action '{actionTitle}' with parameters: {parameters}.");
        }

        private MethodInfo FindMethodByActionTitleInBlock(string blockName, string actionTitle, object[] parameters)
        {
            var block = GetElementByTitle(blockName);
            // Ищем метод в блоке.
            var searchedMethod = block.GetType().GetMethods(BindingFlags.DeclaredOnly).FirstOrDefault(method =>
                method.GetCustomAttribute(typeof(ActionTitleAttribute)) is ActionTitleAttribute attr && attr.ActionTitle == actionTitle && CoreFunctions.CheckParamsTypesOfMethod(parameters, method.GetParameters()));
            return searchedMethod;
           }

        public object GetElementByTitle(string elementTitle)
        {
            if (!IsUsedBlock)
            {
                var element = PageManager.PageContext.Elements.FirstOrDefault(ele => ele.Value == elementTitle).Key;
                if (element != null) return element;
                throw new NullReferenceException(
                    $"Cant find element with title '{elementTitle}' in page {PageManager.PageContext.CurrentPage.GetType()}");
            }
            var blockName =
                ((ElementTitleAttribute) _usedBlock.GetType().GetCustomAttribute(typeof(ElementTitleAttribute)))
                .Name;
            var elementInBlock = PageManager.PageContext.ElementsInBlocks.FirstOrDefault(b => b.Key == blockName).Value
                .FirstOrDefault(ele => ele.Value == elementTitle).Key;
            if (elementInBlock != null) return elementInBlock;
            throw new NullReferenceException(
                $"Cant find element with name '{elementTitle}' in block '{blockName}' at page {PageManager.PageContext.CurrentPage}");

        }


        [ActionTitle("заполняет поле")]
        public virtual void FillField(string elementTitle, string value)
        {
            var element = (ImlElement) GetElementByTitle(elementTitle);
            element.SendKeys(value);
        }

        [ActionTitle("нажимает кнопку")]
        public virtual void PressButton(string elementTitle)
        {
            var element = (ImlElement) GetElementByTitle(elementTitle);
            element.Click();
        }

        [ActionTitle("проверяет значение элемента")]
        public virtual void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = (ImlElement) GetElementByTitle(elementTitle);
            Assert.AreEqual(expectedValue, element.Text, $"Значение элемента '{elementTitle}' не совпадает с ожидаемым.");
        }


        private static class CoreFunctions
        {
            public static bool CheckParamsTypesOfMethod(object[] methodExpectedParams,
                ParameterInfo[] methodActualParams)
            {
                var listOfParam = methodExpectedParams.Select(param => param.GetType()).ToList();
                var result = true;
                for (var i = 0; i < listOfParam.Count; i++)
                {
                    if (listOfParam[i] != methodActualParams[i].ParameterType) result = false;
                }
                return result;
            }
        }
    }
}
