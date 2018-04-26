using System.Reflection;
using IML_AT_Core.Core;
using IML_AT_Core.CustomElements.Attributes;
using IML_AT_Core.Decorators;
using OpenQA.Selenium.Support.PageObjects;

namespace IML_AT_Core.TestBaseClasses
{
    public abstract class BasePage
    {
        protected BasePage()
        {
            StepRunner.Run($"Инициализируется страница {GetPageName()}", () =>
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ExtendedFieldDecorator()));
        }


        private string GetPageName()
        {
            if (GetType().GetCustomAttribute(typeof(PageTitleAttribute)) is PageTitleAttribute titleAttribute &&
                titleAttribute.Title.Length != 0)
                return titleAttribute.Title;
            return GetType().FullName;


        }
    }
}
