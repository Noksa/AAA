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
        private object usedBlock;
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
            CustomPageFactory.Instance.Members.Clear();
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
            if (method != null) method.Invoke(block.GetType(), parameters);
            IsUsedBlock = true;
            usedBlock = block;
            method = FindMethodByActionTitle(actionTitle, parameters);
            method.Invoke(this, parameters);
            IsUsedBlock = false;
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
            // Ищем блок.
            //var block = CoreFunctions.FindBlockByTitle(blockName);
            var block = GetElementByTitle(blockName);
            // Ищем метод в блоке . Если там его нет - ищем метод на странице. Если и там его нет - ищем в базовом классе.
            var searchedMethod = block.GetType().GetMethods().FirstOrDefault(method =>
                method.GetCustomAttribute(typeof(ActionTitleAttribute)) is ActionTitleAttribute attr && attr.ActionTitle == actionTitle && CoreFunctions.CheckParamsTypesOfMethod(parameters, method.GetParameters()));
            return searchedMethod;
           }

        public object GetElementByTitle(string elementTitle)
        {
            if (!IsUsedBlock)
            {
                foreach (var member in CustomPageFactory.Instance.Members)
                {
                    if (member.Value == elementTitle) return member.Key;
                }

                throw new NullReferenceException(
                    $"Cant find element with title '{elementTitle}' in page {CustomPageFactory.Instance.CurrentPage.GetType()}");
            }
            else return GetElementInBlockByTitle(usedBlock, elementTitle);
        }


        public object GetElementInBlockByTitle(object block, string elementTitle)
        {
            var elementsInBlock = CoreFunctions.GetMembersInBlock(block);
            foreach (var element in elementsInBlock)
            {
                if (element.GetType().GetCustomAttribute(typeof(ElementTitleAttribute)) is ElementTitleAttribute attr &&
                    attr.Name == elementTitle) return element;
            }
            throw new NullReferenceException($"Cant find element with name '{elementTitle}' in block '{block}' at page {CustomPageFactory.Instance.CurrentPage}");
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

            public static IEnumerable<object> GetMembersInBlock(object block)
            {
                var list = new List<object>();
                var members = block.GetType().GetMembers().Where(_ => _.DeclaringType == block.GetType());
                members.ToList().ForEach( m => list.Add(m.GetType()));
                return list;
            }
        }
    }
}
