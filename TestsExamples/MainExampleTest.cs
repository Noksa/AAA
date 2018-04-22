using System;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions;
using IML_AT_Core.TestBaseClasses;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using TestsExamples.Pages;

namespace TestsExamples
{
    public class MainExampleTest : BaseTestConfig
    {
       [TestCase("Тест")]
       [TestCase("Оля привет!")]
       [TestCase("Зашли?")]
       [TestCase("Не верный логин или пароль!")]
        public void TestBadLogin(string text)
        {
            var mainPage = new MainPage();
            var loginPage = mainPage.ClickOnLoginButton();
            loginPage.TypeLoginAndPassword("Test", "Hello");
            loginPage.ClickEnterButton();
            loginPage.CheckErrorMsg(text);

        }
    }
}
