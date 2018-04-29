using System;
using AT_Core_Specflow;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using SpecFlowTests.Blocks.Common;

namespace SpecFlowTests.Pages
{
    [PageTitle("Главная")]
    public class MainPage : BasePage
    {
        [FindBy(XPath = "//a[@class = \'login\']")]
        [ElementTitle("Войти")]
        private ImlButton loginButton;
        
        

        public LoginPage ClickOnLoginButton()
        {
            loginButton.Click();
            return new LoginPage();
        }

        public override void FillField(string elementTitle, string value)
        {
            Console.WriteLine("test");
            base.FillField(elementTitle, value);
        }

        private ContactInformationBlock contactInformation;
    }
}
