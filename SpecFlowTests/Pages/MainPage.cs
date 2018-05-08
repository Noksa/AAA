using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using SpecFlowTests.Blocks.Common;
#pragma warning disable 169

namespace SpecFlowTests.Pages
{
    [PageTitle("Главная")]
    public class MainPage : BasePage
    {
        [FindBy(XPath = "//a[@class = \'login\']")] [ElementTitle("Войти")]
        private AButton _loginButton;

        private ContactInformationBlock _contactInformation;
        private NavigationBlock _navigation;
    }
}
