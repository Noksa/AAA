using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.Decorators;
using AT_Core_Specflow.Extensions;
using AT_Core_Specflow.Extensions.WaitExtensions;
using AT_Core_Specflow.Helpers;
using AT_Core_Specflow.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.Core
{
    public abstract class BasePage
    {
        protected bool IsUsedBlock { get; }
        private readonly ABlockElement _usedBlock;

        protected BasePage()
        {
            if (GetType().BaseType == typeof(ABlockElement))
            {
                IsUsedBlock = true;
                _usedBlock = (ABlockElement) this;
                return;
            }
            PageManager.PageContext.Elements.Clear();
            PageManager.PageContext.ElementsInBlocks.Clear();
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new AFieldDecorator());
        }

        public void ExecuteMethodByTitle(string actionTitle, params object[] parameters)
        {
            var method = HelpFunc.FindMethod(this, actionTitle, parameters);
            if (method == null)
                throw new NullReferenceException(
                    $"Cant find method for action '{actionTitle}' at page '{PageManager.PageContext.PageTitle}' with parameters: '{parameters}'.");
            try
            {
                method.Invoke(this, parameters);
            }
            catch (TargetInvocationException ex)
            {
                HelpFunc.TakeScreenshotAndThrowRealEx(ex);
            }
        }

        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle, params object[] parameters)
        {

            var block = GetElementByTitle(blockName);
            var method = HelpFunc.FindMethod(block, actionTitle, parameters);
            if (method == null)
                throw new NullReferenceException(
                    $"Cant find method for action '{actionTitle}' in block '{_usedBlock.Title}' at page '{PageManager.PageContext.PageTitle}' with parameters: '{parameters}'.");
            try
            {
                method.Invoke(block, parameters);
            }
            catch (TargetInvocationException ex)
            {
                HelpFunc.TakeScreenshotAndThrowRealEx(ex);
            }
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
            var blockName = _usedBlock.Title;
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
            var element = (AElement) GetElementByTitle(elementTitle);
            element.SendKeys(value);
            AllureSteps.AddSingleStep($"Поле '{elementTitle}' заполнено значением '{value}'");
        }

        [ActionTitle("нажимает кнопку")]
        [ActionTitle("кликает по ссылке")]
        public virtual void PressButton(string elementTitle)
        {
            var element = (AElement) GetElementByTitle(elementTitle);
            element.Click();
            AllureSteps.AddSingleStep($"Произведён клик по элементу '{elementTitle}'");
        }

        [ActionTitle("проверяет наличие элемента")]
        public virtual void CheckElementExists(string elementTitle)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var result = element.Wait().Until(_ => _.Exists());
            Assert.IsTrue(result, $"Element '{elementTitle}' not exists.");
        }

        public virtual void CheckElementExists(List<object> elementTitles)
        {
            Assert.Multiple(() =>
            {
                foreach (var elementTitle in elementTitles)
                {
                    var element = GetElementByTitle(elementTitle.ToString()) as IWebElement;
                    var result =  element.Wait().Until(_ => _.Exists());
                    Assert.IsTrue(result, $"Element '{elementTitle}' not exists.");
                    AllureSteps.AddSingleStep($"Проверено наличие элемента '{elementTitle}'");
                }
            });
        }

        [ActionTitle("запоминает значение")]
        public virtual void WriteValueToStash(string value, string variable)
        {
            CoreSteps.ScenarioContext.Add(variable, value);
        }
        [ActionTitle("запоминает значение элемента")]
        public virtual void WriteElementValueToStash(string elementTitle, string variable)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var value = element.GetValue();
            CoreSteps.ScenarioContext.Add(variable, value);
        }

        [ActionTitle("проверяет значение элемента")]
        public virtual void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            Assert.AreEqual(expectedValue, element.GetValue(), $"Значение элемента '{elementTitle}' не совпадает с ожидаемым.");
            AllureSteps.AddSingleStep($"Проверено значение элемента '{elementTitle}' со значением '{expectedValue}'");
        }

        [ActionTitle("отмечает чек-бокс")]
        public virtual void SetCheckBox(string elementTitle)
        {
            var checkbox = GetElementByTitle(elementTitle) as IWebElement;
            if (!checkbox.Selected) checkbox.Click();
        }

        #endregion

        #region Private Class with help functions
        private static class HelpFunc
        {
            private static bool CheckParamsTypesOfMethod(object[] methodExpectedParams,
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

            public static MethodInfo FindMethod(object obj, string actionTitle, object[] parameters)
            {
                var allMethods = obj.GetType().GetMethods();
                var methodName = allMethods.SingleOrDefault(m =>
                    m.GetCustomAttributes(typeof(ActionTitleAttribute)) is IEnumerable<ActionTitleAttribute>
                        attr && attr.Any(_ => _.ActionTitle == actionTitle))?.Name;
                var method = allMethods.SingleOrDefault(_ =>
                    _.Name == methodName && CheckParamsTypesOfMethod(parameters, _.GetParameters()));
                return method;
            }

            public static void TakeScreenshotAndThrowRealEx(Exception ex)
            {
                DriverFactory.MakeScreenshot();
                throw ex.GetBaseException();
            }
        }
        #endregion
    }
}
