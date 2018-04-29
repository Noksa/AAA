using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;
using ImlFieldDecorator = AT_Core_Specflow.Decorators.ImlFieldDecorator;

namespace AT_Core_Specflow
{
    public abstract class BasePage
    {
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


        private MethodInfo FindMethodByActionTitle(string actionTitle, object[] parameters)
        {
            // Ищем метод на дочерней странице. Если там его нет - ищем метод в базовом классе.
            var searchedMethod = GetType().GetMethods().FirstOrDefault(method => 
             method.GetCustomAttribute(typeof(ActionTitleAttribute)) is ActionTitleAttribute attr && attr.ActionTitle == actionTitle && CoreFunctions.CheckParamsTypesOfMethod(parameters, method.GetParameters()));
            if (searchedMethod != null) return searchedMethod;
            throw  new NullReferenceException($"Cant find method for action '{actionTitle}' with parameters: {parameters}.");
        }


        [ActionTitle("заполняет поле")]
        public virtual void FillField(string elementTitle, string value)
        {
            var element = (ImlElement) CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            element.SendKeys(value);
        }

        [ActionTitle("нажимает кнопку")]
        public virtual void PressButton(string elementTitle)
        {
            var element = (ImlElement)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            element.Click();
        }

        [ActionTitle("проверяет значение элемента")]
        public virtual void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = (ImlElement)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
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
