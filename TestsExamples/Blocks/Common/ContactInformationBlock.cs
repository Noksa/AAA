using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.CustomElements;
using IML_AT_Core.CustomElements.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsExamples.Blocks.Common
{
    [FindBy(ClassName = "block9")]
    [ElementTitle("Блок \"Контактная информация\"")]
    public class ContactInformationBlock : ImlBlockElement
    {
        public ContactInformationBlock(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle) : base(locator, bys, cache, elementTitle)
        {
        }
    }
}
