using System.Collections.Generic;
using IML_AT_Core.CustomElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements.Elements
{
    public class ImlTextInput : ImlElement
    {
        public ImlTextInput(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }
    }
}
