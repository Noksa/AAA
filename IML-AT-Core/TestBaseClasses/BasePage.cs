
using IML_AT_Core.Core;
using IML_AT_Core.Decorators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace IML_AT_Core.TestBaseClasses
{
    public abstract class BasePage
    {
        public BasePage()
        {
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ExtendedFieldDecorator());
        }
    }
}
