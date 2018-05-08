using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements.Elements
{
    public class ACheckBox : AProxyElement
    {
        public ACheckBox(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }

        public void Select(bool select = true)
        {
            if (!Selected && select) Click();
            if (Selected && !select) Click();
        }
    }
}
