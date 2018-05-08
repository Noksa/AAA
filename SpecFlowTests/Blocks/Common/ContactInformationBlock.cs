using System.Collections.Generic;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.CustomElements.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
#pragma warning disable 169

namespace SpecFlowTests.Blocks.Common
{
    [FindBy(ClassName = "block9")]
    [BlockTitle("Контактная информация")]
    public class ContactInformationBlock : ABlock
    {
        [ElementTitle("Адрес")] [FindBy(XPath = "//p[@class = \'home\']")]
        private ATextLabel _address;



        public ContactInformationBlock(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) :
            base(locator, bys, cache, elementTitle)
        {
        }
    }
}
