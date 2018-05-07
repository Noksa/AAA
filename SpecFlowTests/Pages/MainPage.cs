using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using AT_Core_Specflow.Extensions.WaitExtensions;
using NUnit.Framework;
using SpecFlowTests.Blocks.Common;

namespace SpecFlowTests.Pages
{
    [PageTitle("Главная")]
    public class MainPage : BasePage
    {
        [FindBy(XPath = "//a[@class = \'login\']")]
        [ElementTitle("Войти")]
        private AButton loginButton;

        private ContactInformationBlock _contactInformation;
        private NavigationBlock _navigation;
    }
}
