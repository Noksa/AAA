using System;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestsExamples
{
    public class MainExampleTest : BaseTestConfig
    {

        [Test]
        public void TestOne()
        {
            DriverFactory.GetDriver().Wait(TimeSpan.FromSeconds(5)).Until(_ => _.Page().UrlContain("test"));
            var element = DriverFactory.GetDriver().FindElement(By.XPath("//*[text() = \"Смотреть все новости\" and @href = \"/news\"]"));
        }
    }
}
