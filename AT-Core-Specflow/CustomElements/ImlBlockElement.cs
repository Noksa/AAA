using System.Collections.Generic;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.Decorators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements
{
    public abstract class ImlBlockElement : ImlElement
    {
        protected ImlBlockElement(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
            PageManager.Instance.DecoratingBlock = this;
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ImlFieldDecorator());
            PageManager.Instance.DecoratingBlock = null;
        }
    }
}
