using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
#pragma warning disable 169

namespace SpecFlowTests.Pages
{
    [PageTitle("Авторизация")]
    public class LoginPage : BasePage
    {
        [FindBy(Id = "txtUser")] [ElementTitle("Логин")]
        private ATextInput _login;

        [FindBy(Id = "txtPass")] [ElementTitle("Пароль")]
        private ATextInput _password;

        [FindBy(Id = "Button1")] [ElementTitle("Войти")]
        private AButton _enterButton;

        [FindBy(Id = "errorMsg")] [ElementTitle("Текст ошибки")]
        private ATextLabel _errorMsg;
    }
}
