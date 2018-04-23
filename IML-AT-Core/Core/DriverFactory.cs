using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using IML_AT_Core.Extensions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;


namespace IML_AT_Core.Core
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
                case Browser.chrome:
                    Driver.Value = new ChromeDriver();
                    break;
                case Browser.firefox:
                    Driver.Value = new FirefoxDriver();
                    break;
                case Browser.ie:
                    Driver.Value = new InternetExplorerDriver();
                    break;
                case Browser.safari:
                    Driver.Value = new SafariDriver();
                    break;
                case Browser.opera:
                    Driver.Value = new OperaDriver();
                    break;
                default:
                    throw new ArgumentNullException("Неизвестный тип драйвера \"" + browserType +
                                                    "\". Невозможно проинициализировать драйвер.");
            }
            Driver.Value.Manage().Window.Maximize();
            Driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }


        public static void Dispose()
        {
            if (Driver.Value == null) return;
            Driver.Value.Quit();
            Driver.Value = null;
        }
    }
}