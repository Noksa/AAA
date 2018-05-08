using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Allure.Commons;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using AT_Core_Specflow.Decorators;
using AT_Core_Specflow.Extensions;
using AT_Core_Specflow.Extensions.WaitExtensions;
using AT_Core_Specflow.Helpers;
using AT_Core_Specflow.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

#pragma warning disable 618

namespace AT_Core_Specflow.Core
{
    public abstract class BasePage
    {
        #region Fields

        protected bool IsUsedBlock { get; }
        private readonly ABlock _usedBlock;

        #endregion

        #region Properties

        public IWebElement BodyElement => DriverFactory.GetDriver().FindElement(By.TagName("body"));

        #endregion

        #region Ctor
        protected BasePage()
        {
            if (GetType().BaseType == typeof(ABlock))
            {
                IsUsedBlock = true;
                _usedBlock = (ABlock) this;
                return;
            }
            PageManager.PageContext.Elements.Clear();
            PageManager.PageContext.ElementsInBlocks.Clear();
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new AFieldDecorator());
        }
        #endregion

        #region Core Methods
        public void ExecuteMethodByTitle(string actionTitle, params object[] parameters)
        {
            var method = HelpFunc.FindMethod(this, actionTitle, parameters);
            if (method == null)
                throw new NullReferenceException(
                    $"Не найден метод для выполнения действия '{actionTitle}' на странице '{PageManager.PageContext.PageTitle}'.\nПараметры метода: '{string.Join(", ", parameters)}'.");
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
                    $"Не найден метод для выполнения действия '{actionTitle}' в блоке '{_usedBlock.Title}' на странице '{PageManager.PageContext.PageTitle}'.\nПараметры метода: '{string.Join(", ", parameters)}'.");
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
                    $"Не найден элемент с названием '{elementTitle}' на странице '{PageManager.PageContext.PageTitle}'.");
            }
            var blockName = _usedBlock.Title;
            var elementInBlock = PageManager.PageContext.ElementsInBlocks.FirstOrDefault(b => b.Key == blockName).Value
                .FirstOrDefault(ele => ele.Value == elementTitle).Key;
            if (elementInBlock != null) return elementInBlock;
            throw new NullReferenceException(
                $"Не найден элемент с названием '{elementTitle}' в блоке '{blockName}' на странице '{PageManager.PageContext.PageTitle}'.");

        }
        #endregion

        #region Actions

        [ActionTitle("заполняет поле")]
        public virtual void FillField(string elementTitle, string value)
        {
            var element = (AProxyElement) GetElementByTitle(elementTitle);
            element.Clear();
            element.SendKeys(value);
            AllureSteps.AddSingleStep($"Поле '{elementTitle}' заполнено значением '{value}'.");
        }
        
        public virtual void FillField(Dictionary<string, string> dictionary)
        {
            foreach (var keyValuePair in dictionary)
            {
                var element = GetElementByTitle(keyValuePair.Key) as IWebElement;
                element.SendKeys(keyValuePair.Value);
                AllureSteps.AddSingleStep($"Поле '{GetElementByTitle(keyValuePair.Key)}' заполнено значением '{keyValuePair.Value}'.");
            }
        }


        [ActionTitle("нажимает клавишу")]
        public virtual void PressButton(string btn)
        {
            HelpFunc.Actions().SendKeys(btn);
        }

        [ActionTitle("нажимает кнопку")]
        [ActionTitle("кликает по ссылке")]
        public virtual void ClickButton(string elementTitle)
        {
            var element = (AProxyElement) GetElementByTitle(elementTitle);
            element.Click();
            AllureSteps.AddSingleStep($"Произведён клик по элементу '{elementTitle}'.");
        }

        [ActionTitle("проверяет наличие элемента")]
        public virtual void CheckElementExists(string elementTitle)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var result = element.Wait().Until(_ => _.Exists());
            Assert.IsTrue(result, $"Element '{elementTitle}' not exists.");
            AllureSteps.AddSingleStep($"Проверено наличие элемента '{elementTitle}'.");
        }

        [ActionTitle("проверяет отсутствие элемента")]
        public virtual void CheckElementNotExists(string elementTitle)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var result = element.Wait().Until(_ => !_.Exists());
            Assert.IsTrue(result, $"Element '{elementTitle}' exists.");
            AllureSteps.AddSingleStep($"Проверено отсутствие элемента '{elementTitle}'.");
        }

        [ActionTitle("проверяет наличие элемента с текстом")]
        public virtual void CheckElementsExistsWithText(string elementTitle, string text)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var elementText = element.GetValue();
            Assert.AreEqual(text, elementText, $"Текст элемента '{elementTitle}' не соответствует ожидаемому.");
        }

        public virtual void CheckElementExists(List<object> elementTitles)
        {
            Assert.Multiple(() =>
            {
                foreach (var elementTitle in elementTitles)
                {
                    var element = GetElementByTitle(elementTitle.ToString()) as IWebElement;
                    var result =  element.Wait().Until(_ => _.Exists());
                    if (result) AllureSteps.AddSingleStep($"Проверено наличие элемента '{elementTitle}'.");
                    else AllureSteps.AddSingleStep($"Элемент '{elementTitle}' отсутствует.", Status.failed);
                    Assert.IsTrue(result, $"Элемент '{elementTitle}' отсутствует.");
                }
            });
        }

        [ActionTitle("запоминает значение")]
        public virtual void WriteValueToStash(string value, string variable)
        {
            CoreSteps.ScenarioContext.Add(variable, value);
            AllureSteps.AddSingleStep($"Значение '{value}' записано в переменную '{variable}'.");
        }
        [ActionTitle("запоминает значение элемента")]
        public virtual void WriteElementValueToStash(string elementTitle, string variable)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            var value = element.GetValue();
            CoreSteps.ScenarioContext.Add(variable, value);
            AllureSteps.AddSingleStep($"Значение элемента '{elementTitle}' записано в переменную '{variable}'.");
        }

        [ActionTitle("проверяет значение элемента")]
        public virtual void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = GetElementByTitle(elementTitle) as IWebElement;
            Assert.AreEqual(expectedValue, element.GetValue(), $"Значение элемента '{elementTitle}' не совпадает с ожидаемым.");
            AllureSteps.AddSingleStep($"Проверено значение элемента '{elementTitle}' со значением '{expectedValue}'.");
        }

        [ActionTitle("проверяет наличие текста на странице")]
        public virtual void CheckTextPresentsOnPage(string text)
        {
            Assert.True(BodyElement.Wait().Until(_ => _.Text.ToLower().Contains(text.ToLower())), $"Текст '{text}' не появился на странице '{PageManager.PageContext.PageTitle}'.");
            AllureSteps.AddSingleStep($"Текст '{text}' присутствует на странице '{PageManager.PageContext.PageTitle}'.");
        }

        [ActionTitle("проверяет отсутствие текста на странице")]
        public virtual void CheckTextNotPresentsOnPage(string text)
        {
            Assert.True(BodyElement.Wait().Until(_ => !_.Text.ToLower().Contains(text.ToLower())), $"Текст '{text}' присутствует на странице '{PageManager.PageContext.PageTitle}'.");
            AllureSteps.AddSingleStep($"Текст '{text}' отсутствует на странице '{PageManager.PageContext.PageTitle}'.");
        }

        [ActionTitle("отмечает чек-бокс")]
        public virtual void SetCheckBox(string elementTitle)
        {
            var checkbox = GetElementByTitle(elementTitle) as ACheckBox;
            checkbox.Select();
            AllureSteps.AddSingleStep($"Чек-бокс '{elementTitle}' отмечен.");
        }

        [ActionTitle("снимает чек-бокс")]
        public virtual void UnsetCheckBox(string elementTitle)
        {
            var checkbox = GetElementByTitle(elementTitle) as ACheckBox;
            checkbox.Select(false);
            AllureSteps.AddSingleStep($"Чек-бокс '{elementTitle}' снят.");
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

            public static Actions Actions()
            {
                var actions = new Actions(DriverFactory.GetDriver());
                return actions;
            }
        }
        #endregion
    }
}
