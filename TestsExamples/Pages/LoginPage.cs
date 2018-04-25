using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Commons;
using IML_AT_Core.Core;
using IML_AT_Core.CustomElements.Attributes;
using IML_AT_Core.CustomElements.Elements;
using IML_AT_Core.TestBaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
// ReSharper disable InconsistentNaming

namespace TestsExamples.Pages
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "txtUser")]
        [ElementTitle("Логин")]
        private ImlTextInput login;

        [FindsBy(How = How.Id, Using = "txtPass")]
        [ElementTitle("Пароль")]
        private ImlTextInput password;

        [FindsBy(How = How.Id, Using = "Button1")]
        [ElementTitle("Войти")]
        private ImlButton enterButton;

        [FindsBy(How = How.Id, Using = "errorMsg")]
        [ElementTitle("Текст ошибки")]
        private ImlTextLabel errorMsg;
        
        

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
           StepRunner.Run("Проверка сообщения ошибки", () => Assert.AreEqual(errorMsg.Text, expectedText, $"Текст ошибки не соответствует ожидаемому."));
        }
    }
}
