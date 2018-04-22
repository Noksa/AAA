using System;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace IML_AT_Core.Core
{
    public abstract class BaseTestConfig
    {
        public TestContext TestContext => TestContext.CurrentContext;
        protected Browser Browser;
        protected string Url;

        [SetUp]
        public virtual void Setup()
        {
            var parsed = Enum.TryParse(ConfigurationManager.AppSettings.Get("BrowserType").ToLower(), out Browser);
            if (!parsed)
                throw new NullReferenceException(
                    "Не обранужен тип браузера в App.config!\nДобавьте тип браузера в файл конфигурации. Key: BrowserType");
            Url = ConfigurationManager.AppSettings.Get("StartUrl");
            if (string.IsNullOrEmpty(Url))
                throw new NullReferenceException(
                    "Не обнаружена стартовая страница в файле конфигурации App.config!\nДобавьте стартовую страницу в файл конфигурации. Key: StartUrl");
            DriverFactory.InitDriver(Browser);
            DriverFactory.GetDriver().Navigate().GoToUrl(Url);
        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                if (TestContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed) return;
                var timestamp = DateTime.Now.ToString("dd-MM-yyyy-hhmmss");
                DriverFactory.GetDriver().TakeScreenshot()
                    .SaveAsFile($"C:\\Temp\\{timestamp}.png", ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment($"C:\\Temp\\{timestamp}.png");
            }
            finally
            {
                DriverFactory.Dispose();
            }
        }
    }
}