using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Commons;
using IML_AT_Core.Core;
using IML_AT_Core.CustomElements.Elements;
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
        private ImlButton enterButton;

        [FindsBy(How = How.Id, Using = "errorMsg")]
        private IWebElement errorMsg;
        
        

        public void TypeLoginAndPassword(string login, string password)
        {
            StepRunner.Run("Заполняем поле логин и пароль", () =>
            {
                this.login.SendKeys(login);
                this.password.SendKeys(password);
            });
        }

        public void ClickEnterButton()
        {
            StepRunner.Run("Нажимаем кнопку \"Войти\"", () => enterButton.Click());
        }

        public void CheckErrorMsg(string expectedText)
        {
           Assert.AreSame(errorMsg.Text, expectedText, $"Текст ошибки не соответствует ожидаемому.");
        }
    }
}
