
using OpenQA.Selenium;

namespace IML_AT_Core.TestBaseClasses
{
    public abstract class BasePage
    {
        public virtual void FillField(IWebElement element, string text)
        {
            element.SendKeys(text);
        }

        public virtual void ClickOnElement(IWebElement element)
        {
            element.Click();
        }
    }
}
