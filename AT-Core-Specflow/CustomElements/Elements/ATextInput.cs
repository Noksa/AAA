using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements.Elements
{
    public class ATextInput : AProxyElement
    {
        public ATextInput(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }
    }
}
