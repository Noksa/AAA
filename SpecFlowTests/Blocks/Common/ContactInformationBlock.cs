
using System.Collections.Generic;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowTests.Blocks.Common
{
    [FindBy(ClassName = "block9")]
    [ElementTitle("Контактная информация")]
    public class ContactInformationBlock : ImlBlockElement
    {
        public ContactInformationBlock(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }

        [ElementTitle("Адрес")]
        [FindBy(XPath = "//p[@class = \'home\']")]
        private ImlTextLabel address;

        [ElementTitle("Адрес2")]
        [FindBy(XPath = "//p[@class = \'home\']")]
        private ImlTextLabel addresds;
    }

}
