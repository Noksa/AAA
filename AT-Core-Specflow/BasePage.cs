using System;
using System.Linq;
using System.Reflection;
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
            var method = FindMethodByActionTitle(actionTitle);
            method.Invoke(this, parameters);
        }


        private MethodInfo FindMethodByActionTitle(string actionTitle)
        {
            // find the name of method in base class
            var methodName = "";
            var listOfBaseMethods = typeof(BasePage).GetMethods().ToList();
            foreach (var method in listOfBaseMethods)
            {
                if (method.GetCustomAttribute(typeof(ActionTitleAttribute), true) is ActionTitleAttribute
                        actionTitleAttribute && actionTitleAttribute.ActionTitle == actionTitle)
                {
                    methodName = method.Name;
                    break;
                }
            }

            return FindMethodByName(methodName);
        }

        private MethodInfo FindMethodByName(string methodName)
        {
            var theMethod = GetType().GetMethod(methodName);
            if (theMethod != null && theMethod.DeclaringType == GetType()) return theMethod;
            theMethod = typeof(BasePage).GetMethod(methodName);
            if (theMethod != null) return theMethod;
            return null;
        }


        [ActionTitle("заполняет поле")]
        public void FillField(string elementTitle, string value)
        {
            var element = (ImlElement) CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            element.SendKeys(value);
        }

        [ActionTitle("нажимает кнопку")]
        public void PressButton(string elementTitle)
        {
            var element = (ImlElement)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            element.Click();
        }

        [ActionTitle("проверяет значение элемента")]
        public void CheckElementValue(string elementTitle, string expectedValue)
        {
            var element = (ImlElement)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            Assert.AreEqual(expectedValue, element.Text, $"Значение элемента '{elementTitle}' не совпадает с ожидаемым.");
        }
    }
}
