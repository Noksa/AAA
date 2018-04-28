using AT_Core_Specflow;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;

namespace SpecFlowTests.Pages
{
    [PageTitle("Главная")]
    public class MainPage : BasePage
    {
        [FindBy(XPath = "//a[@class = \'login\']")]
        [ElementTitle("Войти")]
        private ImlButton loginButton;
        
        

        public LoginPage ClickOnLoginButton()
        {
            loginButton.Click();
            return new LoginPage();
        }
        
        
    }
}
