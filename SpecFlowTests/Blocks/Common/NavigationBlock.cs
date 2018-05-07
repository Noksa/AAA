using System.Collections.Generic;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowTests.Blocks.Common
{
    [BlockTitle("Навигация")]
    [FindBy(Css = "div[class=\'block1\']")]
    public class NavigationBlock : AoBlockElement
    {
        public NavigationBlock(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }
        [FindBy(Css = "a[href=\'/about/history\']")]
        [ElementTitle("О компании")]
        private AoLink aboutCompany;

        [FindBy(XPath = "//a[@href=\'/specials/c2c\']")]
        [ElementTitle("Частным лицам")]
        private AoLink individuals;

    }
}
