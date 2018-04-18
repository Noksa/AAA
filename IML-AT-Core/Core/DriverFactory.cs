using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace IML_AT_Core.Core
{
    public abstract class DriverFactory
    {
        private static ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver()
        {
            if (!_driver.IsValueCreated)
                throw new NullReferenceException(
                    "Драйвер не был инициализирован. Проинициализируйте драйвер перед его использованием, путём вызова метода DriverFactory.InitDriver(driverType)");
            return _driver.Value;
        }

        public static void InitDriver(Browser browserType)
        {
            switch (browserType)
            {
                case Browser.Chrome:
                    _driver.Value = new ChromeDriver();
                    break;
                case Browser.Firefox:
                    _driver.Value = new FirefoxDriver();
                    break;
                case Browser.InternetExplorer:
                    _driver.Value = new InternetExplorerDriver();
                    break;
                case Browser.Safari:
                    _driver.Value = new SafariDriver();
                    break;
                case Browser.Opera:
                    _driver.Value = new OperaDriver();
                    break;
                default:
                    throw new ArgumentNullException("Неизвестный тип драйвера \"" + browserType +
                                                    "\". Невозможно проинициализировать драйвер.");
            }

            _driver.Value.Manage().Window.Maximize();
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void Dispose()
        {
            if (_driver.Value == null && _driver == null) return;
            _driver.Value.Close();
            _driver.Value = null;
            _driver.Dispose();
            _driver = null;
        }
    }
}