using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowTests.Pages
{
    [PageTitle("Авторизация")]
    public class LoginPage : BasePage
    {
        [FindBy(Id = "txtUser")]
        [ElementTitle("Логин")]
        private ATextInput login;

        [FindBy(Id = "txtPass")]
        [ElementTitle("Пароль")]
        private ATextInput password;

        [FindBy(Id = "Button1")]
        [ElementTitle("Войти")]
        private AButton enterButton;

        [FindBy(Id = "errorMsg")]
        [ElementTitle("Текст ошибки")]
        private ATextLabel errorMsg;
        
    }
}
