using Allure.Commons;
using IML_AT_Core.TestBaseClasses;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using TestsExamples.Pages;

namespace TestsExamples
{
    [TestFixture]
    [AllureFixture("Test")]
    public class MainExampleTest : BaseTestConfig
    {

        private const string ErrorMsg = "Не верный логин или пароль!";

        [Parallelizable(ParallelScope.Children)]
        [TestCase("Ru", "Ru")]
        [TestCase("Ру", "Ру")]
        [TestCase(" ", " ")]
        [TestCase(" ", "")]
        [AllureTest("Вход в личный кабинет")]
        [AllureSeverity(SeverityLevel.Blocker)]
        [AllureStory("Негативные сценарии")]
        public void TestBadLogin(string login, string password)
        {
            var mainPage = new MainPage();
            var loginPage = mainPage.ClickOnLoginButton();
            loginPage.TypeLoginAndPassword(login, password);
            loginPage.ClickEnterButton();
            loginPage.CheckErrorMsg(ErrorMsg);
        }
    }
}


