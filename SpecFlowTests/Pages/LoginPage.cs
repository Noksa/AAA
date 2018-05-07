using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;

namespace SpecFlowTests.Pages
{
    [PageTitle("Авторизация")]
    public class LoginPage : BasePage
    {
        [FindBy(Id = "txtUser")]
        [ElementTitle("Логин")]
        private AoTextInput login;

        [FindBy(Id = "txtPass")]
        [ElementTitle("Пароль")]
        private AoTextInput password;

        [FindBy(Id = "Button1")]
        [ElementTitle("Войти")]
        private AoButton enterButton;

        [FindBy(Id = "errorMsg")]
        [ElementTitle("Текст ошибки")]
        private AoTextLabel errorMsg;
    }
}
