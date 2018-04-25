using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Core;
using IML_AT_Core.Core.Interfaces;
using IML_AT_Core.CustomElements.Elements;
using IML_AT_Core.TestBaseClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsExamples.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@class = \'login\']")]
        private ImlButton _loginButton;

        public LoginPage ClickOnLoginButton()
        {
            StepRunner.Run("Нажимаем на кнопку входа", () => _loginButton.Click());
            return new LoginPage();
        }
        
        
    }
}
