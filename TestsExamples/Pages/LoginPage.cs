using IML_AT_Core.Core;
using IML_AT_Core.CustomElements.Attributes;
using IML_AT_Core.CustomElements.Elements;
using IML_AT_Core.TestBaseClasses;
using NUnit.Framework;

namespace TestsExamples.Pages
{
    public class LoginPage : BasePage
    {
        [FindBy(Id = "txtUser")]
        [ElementTitle("Логин")]
        private ImlTextInput login;

        [FindBy(Id = "txtPass")]
        [ElementTitle("Пароль")]
        private ImlTextInput password;

        [FindBy(Id = "Button1")]
        [ElementTitle("Войти")]
        private ImlButton enterButton;

        [FindBy(Id = "errorMsg")]
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
           StepRunner.Run("Ожидание появления ошибки и проверка текст ошибки", () => Assert.AreEqual(errorMsg.Text, expectedText, "Текст ошибки не соответствует ожидаемому."));
        }
    }
}
