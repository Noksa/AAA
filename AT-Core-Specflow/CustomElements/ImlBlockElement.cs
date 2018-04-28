using System.Collections.Generic;
using IML_AT_Core.CustomElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements
{
    public abstract class ImlBlockElement : ImlElement
    {
        protected ImlBlockElement(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }
    }
}
