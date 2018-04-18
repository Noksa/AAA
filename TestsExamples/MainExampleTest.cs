

using System;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestsExamples
{
    public class MainExampleTest
    {
        [SetUp]
        public void Setup()
        {
            DriverFactory.InitDriver(Browser.Chrome);
            DriverFactory.GetDriver().Navigate().GoToUrl("http://iml.ru");
        }

        [TearDown]
        public void TearDown()
        {
            DriverFactory.Dispose();
        }

        [Test]
        public void TestOne()
        {
            DriverFactory.GetDriver().Wait(TimeSpan.FromMinutes(1)).UntilPage().ReadyStateComplete();
            var element = DriverFactory.GetDriver().FindElement(By.XPath("//*[text() = \"Смотреть все новости\" and @href = \"/news\"]"));
        }
    }
}
