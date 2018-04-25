using IML_AT_Core.Core;
using IML_AT_Core.Decorators;
using OpenQA.Selenium.Support.PageObjects;

namespace IML_AT_Core.TestBaseClasses
{
    public abstract class BasePage
    {
        protected BasePage()
        {
            StepRunner.Run($"Открывается страница {GetType().FullName}", () =>
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ExtendedFieldDecorator()));
        }
    }
}
