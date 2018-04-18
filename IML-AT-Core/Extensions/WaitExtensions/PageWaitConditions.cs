using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class PageWaitConditions : IPageWaitConditions
    {
        private readonly IWebDriver _driver;
        private readonly TimeSpan _timepsan;
        private WebDriverWait Wait => new WebDriverWait(_driver, _timepsan);

        public PageWaitConditions(IWebDriver driver, TimeSpan timespan)
        {
            _driver = driver;
            _timepsan = timespan;
        }



        public void TitleEqual(string title)
        {
            Wait.Until(ExpectedConditions.TitleIs(title));
        }



        public void TitleContain(string title, bool ignoreCase = false)
        {
            if (ignoreCase) Wait.Until(drv => drv.Title.ToLower().Contains(title.ToLower()));
            else Wait.Until(drv => drv.Title.Contains(title));
        }



        public void UrlEqual(string url)
        {
            Wait.Until(ExpectedConditions.UrlToBe(url));
        }



        public void UrlContain(string url)
        {
            Wait.Until(ExpectedConditions.UrlContains(url));
        }



        public void UrlMatches(string regex)
        {
            Wait.Until(ExpectedConditions.UrlMatches(regex));
        }



        public void ReadyStateComplete()
        {
            Wait.Until(driver =>
                ((IJavaScriptExecutor) driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}




