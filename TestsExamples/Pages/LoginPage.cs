using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Core;
using IML_AT_Core.TestBaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsExamples.Pages
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "txtUser")]
        private IWebElement login;

        [FindsBy(How = How.Id, Using = "txtPass")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "Button1")]
        private IWebElement enterButton;

        [FindsBy(How = How.Id, Using = "errorMsg")]
        private IWebElement errorMsg;

        public LoginPage()
        {
            PageFactory.InitElements(DriverFactory.GetDriver(), this);
        }

        public void TypeLoginAndPassword(string login, string password)
        {
            this.login.SendKeys(login);
            this.password.SendKeys(password);
        }

        public void ClickEnterButton()
        {
            enterButton.Click();
        }

        public void CheckErrorMsg(string expectedText)
        {
           Assert.True(errorMsg.Text == expectedText, $"Текст ошибки не соответствует ожидаемому. Ожидали: \"{expectedText}\". Фактически: \"{errorMsg.Text}\"");
        }
    }
}
