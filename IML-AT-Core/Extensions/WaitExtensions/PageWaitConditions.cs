using System;
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
            _timepsan = timespan;
            _driver = driver;
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


        public bool UrlContain(string url)
        {
            return ExpectedConditions.UrlContains(url).Invoke(_driver);
        }


        public void UrlMatches(string regex)
        {
            Wait.Until(ExpectedConditions.UrlMatches(regex));
        }


        public bool ReadyStateComplete()
        {
            return ((IJavaScriptExecutor) _driver).ExecuteScript("return document.readyState").Equals("complete");
        }


        public void LoaderDissapear()
        {
            // TODO
        }


        public void LoaderDissapear(By by)
        {
            var loader = _driver.FindElement(by);
            loader.Wait(TimeSpan.FromSeconds(1)).Until(_ => _.Displayed);
            loader.Wait(TimeSpan.FromSeconds(30)).Until(_ => !_.Displayed);
        }
    }
}