using IML_AT_Core.CustomElements.Attributes;
using IML_AT_Core.CustomElements.Elements;
using IML_AT_Core.TestBaseClasses;
using OpenQA.Selenium.Support.PageObjects;
using TestsExamples.Blocks.Common;

namespace TestsExamples.Pages
{
    public class MainPage : BasePage
    {
        [FindBy(XPath = "//a[@class = \'login\']")]
        [ElementTitle("Войти")]
        private ImlButton loginButton;
        

        private ContactInformationBlock contactInformation;

        public LoginPage ClickOnLoginButton()
        {
            loginButton.Click();
            return new LoginPage();
        }
        
        
    }
}
