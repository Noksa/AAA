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
        private VTextInput login;

        [FindBy(Id = "txtPass")]
        [ElementTitle("Пароль")]
        private VTextInput password;

        [FindBy(Id = "Button1")]
        [ElementTitle("Войти")]
        private VButton enterButton;

        [FindBy(Id = "errorMsg")]
        [ElementTitle("Текст ошибки")]
        private VTextLabel errorMsg;
    }
}
