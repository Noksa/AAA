using System;
using AT_Core_Specflow;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using IML_AT_Core.Core;
using NUnit.Framework;

namespace SpecFlowTests.Pages
{
    [PageTitle("Авторизация")]
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


        public override void FillField(string elementTitle, string value)
        {
            Console.WriteLine("test");
            base.FillField(elementTitle, value);
        }
    }
}
