using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using VFieldDecorator = AT_Core_Specflow.Decorators.VFieldDecorator;
namespace AT_Core_Specflow.Core
{
    public abstract class BasePage
    {
        protected bool IsUsedBlock { get; set; }
        private readonly object _usedBlock;

        protected BasePage()
        {
            if (GetType().BaseType == typeof(BlockElement))
            {
                IsUsedBlock = true;
                _usedBlock = this;
                return;
            }
            PageManager.PageContext.Elements.Clear();
            PageManager.PageContext.ElementsInBlocks.Clear();
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new VFieldDecorator());
        }

        public void ExecuteMethodByTitle(string actionTitle, params object[] parameters)
        {
            var method = FindMethodByActionTitle(actionTitle, parameters);
            try
            {
                method.Invoke(this, parameters);
            }
            catch (TargetInvocationException ex)
            {
                TakeScreenshotAndThrowRealEx(ex);
            }
        }

        private static void TakeScreenshotAndThrowRealEx(Exception ex)
        {
            DriverFactory.MakeScreenshot();
            throw ex.GetBaseException();
        }

        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle, params object[] parameters)
        {
            try
            {
                var block = GetElementByTitle(blockName);
                var method = FindMethodByActionTitleInBlock(block, actionTitle, parameters);
                if (method != null)
                {
                    method.Invoke(block, parameters);
                    return;
                }
                method = FindMethodByActionTitle(actionTitle, parameters);
                if (method != null)
                {
                    method.Invoke(this, parameters);
                    return;
                }
            }
            catch (TargetInvocationException ex)
            {
                TakeScreenshotAndThrowRealEx(ex);
            }
            throw new NullReferenceException($"Cant find method for action '{actionTitle}' in block '{blockName}' at page '{PageManager.PageContext.PageTitle}' with parameters: {parameters.ToArray()}");
        }

        private MethodInfo FindMethodByActionTitle(string actionTitle, object[] parameters)
        {
            // Ищем метод на дочерней странице. Если там его нет - ищем метод в базовом классе.
            foreach (var method in GetType().GetMethods())
            {
                if (method.GetCustomAttributes(typeof(ActionTitleAttribute)) is IEnumerable<ActionTitleAttribute> attr && attr.Any(titleAttribute => titleAttribute.ActionTitle == actionTitle &&
                                               HelpFunc.CheckParamsTypesOfMethod(parameters, method.GetParameters()))) return method;
            }
            throw  new NullReferenceException($"Cant find method for action '{actionTitle}' at page '{PageManager.PageContext.PageTitle}' with parameters: '{parameters}'.");
        }

        private MethodInfo FindMethodByActionTitleInBlock(object block, string actionTitle, object[] parameters)
        {
            // Ищем метод в блоке.
            foreach (var method in block.GetType().GetMethods())
            {
                if (method.GetCustomAttributes(typeof(ActionTitleAttribute)) is IEnumerable<ActionTitleAttribute> attr && attr.Any(titleAttribute => titleAttribute.ActionTitle == actionTitle &&
                                               HelpFunc.CheckParamsTypesOfMethod(parameters, method.GetParameters()))) return method;
            }
            throw new NullReferenceException($"Cant find method for action '{actionTitle}' in block '{((BlockElement)block).NameOfElement}' at page '{PageManager.PageContext.PageTitle}' with parameters: '{parameters}'.");

        }

        public object GetElementByTitle(string elementTitle)
        {
            if (!IsUsedBlock)
            {
                var element = PageManager.PageContext.Elements.FirstOrDefault(ele => ele.Value == elementTitle).Key;
                if (element != null) return element;
                throw new NullReferenceException(
                    $"Cant find element with title '{elementTitle}' at page '{PageManager.PageContext.PageTitle}'");
            }
            var blockName =
                ((ElementTitleAttribute) _usedBlock.GetType().GetCustomAttribute(typeof(ElementTitleAttribute)))
                .Name;
            var elementInBlock = PageManager.PageContext.ElementsInBlocks.FirstOrDefault(b => b.Key == blockName).Value
                .FirstOrDefault(ele => ele.Value == elementTitle).Key;
            if (elementInBlock != null) return elementInBlock;
            throw new NullReferenceException(
                $"Cant find element with name '{elementTitle}' in block '{blockName}' at page '{PageManager.PageContext.PageTitle}'");

        }


        #region Actions

        [ActionTitle("заполняет поле")]
        public virtual void FillField(string elementTitle, string value)
        {
            var element = (VElement) GetElementByTitle(elementTitle);
            element.SendKeys(value);
        }

        [ActionTitle("нажимает кнопку")]
        [ActionTitle("кликает по ссылке")]
        public virtual void PressButton(string elementTitle)
        {
            var element = (VElement) GetElementByTitle(elementTitle);
            element.Click();
        }

        [ActionTitle("запоминает значение")]
        public virtual void WriteValueToStash(string value, string variable)
        {
            Stash.Add(variable, value);
        }
        [ActionTitle("проверяет значение элемента")]
        public virtual void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = (VElement) GetElementByTitle(elementTitle);
            Assert.AreEqual(expectedValue, element.Text, $"Значение элемента '{elementTitle}' не совпадает с ожидаемым.");
        }

        #endregion

        #region Private Class with help functions
        private static class HelpFunc
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
        #endregion
    }
}
