using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Core;
using IML_AT_Core.TestBaseClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsExamples.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@class = \'login\']")]
        private IWebElement loginButton;

        public LoginPage ClickOnLoginButton()
        {
            loginButton.Click();
            return new LoginPage();
        }

        public MainPage()
        {
           PageFactory.InitElements(DriverFactory.GetDriver(), this);
        }
    }
}
