using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.CustomElements;
using IML_AT_Core.CustomElements.Elements;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ImlButton = AT_Core_Specflow.CustomElements.Elements.ImlButton;
using ImlElement = AT_Core_Specflow.CustomElements.ImlElement;
using ImlTextInput = AT_Core_Specflow.CustomElements.Elements.ImlTextInput;

namespace AT_Core_Specflow
{
    [Binding]
    public class CommonSteps
    {
        [StepDefinition(@"открывается страница ""(.*)""")]
        public void OpenPage(string p0)
        {
            CustomPageFactory.Instance.OpenPage(p0);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle)
        {
            CustomPageFactory.Instance.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\" (?:значением|со значением) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle, string value)
        {
            //var button = (ImlButton)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            //button.Click();
            CustomPageFactory.Instance.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle, value);
        }


    }
}
