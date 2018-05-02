using System;
using System.Configuration;
using System.IO;
using System.Threading;
using IML_AT_Core.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Extensions;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Commons;

namespace AT_Core_Specflow.Core
{
    public abstract class DriverFactory
    {
        private static readonly ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();
        public static IWebDriver GetDriver()
        {
            if (!Driver.IsValueCreated)
                throw new NullReferenceException(
                    "Драйвер не был инициализирован. Проинициализируйте драйвер перед его использованием, путём вызова метода DriverFactory.InitDriver(driverType)");
            return Driver.Value;
        }


        public static void InitDriver(Browser browserType)
        {
            switch (browserType)
            {
                case Browser.Chrome:
                    Driver.Value = new ChromeDriver();
                    break;
                case Browser.Firefox:
                    Driver.Value = new FirefoxDriver();
                    break;
                case Browser.Ie:
                    Driver.Value = new InternetExplorerDriver();
                    break;
                case Browser.Safari:
                    Driver.Value = new SafariDriver();
                    break;
                case Browser.Opera:
                    Driver.Value = new OperaDriver();
                    break;
                default:
                    throw new ArgumentNullException($"Неизвестный тип драйвера \"{browserType}\". Невозможно проинициализировать драйвер.");
            }
            Driver.Value.Manage().Window.Maximize();
            int.TryParse(ConfigurationManager.AppSettings.Get("ImplicitWait"), out var defaultWait);
            if (defaultWait == 0) defaultWait = 60;
            Driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(defaultWait); // hardcode default timeout
        }


        public static void Dispose()
        {
            if (Driver.Value == null) return;
            Driver.Value.Quit();
            Driver.Value = null;
        }

        public static void MakeScreenshot()
        {
            var timestamp = DateTime.Now.ToString("dd-MM-yyyy-hhmmss");
            var pathToFile = Path.Combine(TestContext.CurrentContext.TestDirectory,
                TestContext.CurrentContext.Test.ID + "-" + timestamp + ".png");
            GetDriver().TakeScreenshot()
                .SaveAsFile(pathToFile, ScreenshotImageFormat.Png);
            AllureLifecycle.Instance.AddAttachment(pathToFile, timestamp);
            if (File.Exists(pathToFile)) File.Delete(pathToFile);
        }
    }
}