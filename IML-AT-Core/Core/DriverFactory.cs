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
        private static ThreadLocal<IWebDriver> _driver;
        public static IWebDriver GetDriver()
        {
            if (!_driver.IsValueCreated)
                throw new NullReferenceException(
                    "Драйвер не был инициализирован. Проинициализируйте драйвер перед его использованием, путём вызова метода DriverFactory.InitDriver(driverType)");
            return _driver.Value;
        }


        public static void InitDriver(Browser browserType)
        {if (_driver == null) _driver = new ThreadLocal<IWebDriver>();
            switch (browserType)

            {
                case Browser.chrome:

                    _driver.Value = new ChromeDriver();

                    break;

                case Browser.firefox:

                    _driver.Value = new FirefoxDriver();

                    break;

                case Browser.ie:

                    _driver.Value = new InternetExplorerDriver();

                    break;

                case Browser.safari:

                    _driver.Value = new SafariDriver();

                    break;

                case Browser.opera:

                    _driver.Value = new OperaDriver();

                    break;

                default:

                    throw new ArgumentNullException("Неизвестный тип драйвера \"" + browserType +
                                                    "\". Невозможно проинициализировать драйвер.");
            }


            _driver.Value.Manage().Window.Maximize();

            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var firingDriver = new EventFiringWebDriver(_driver.Value);

            _driver.Value = firingDriver;

            firingDriver.ExceptionThrown += FiringDriver_ExceptionThrown;
        }


        private static void FiringDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)

        {
            // string timestamp = DateTime.Now.ToString("dd-MM-yyyy-hhmmss");

            //   _driver.Value.TakeScreenshot().SaveAsFile(timestamp + ".png", ScreenshotImageFormat.Png);
        }


        public static void Dispose()

        {
            if (_driver.Value == null && _driver == null) return;

            _driver.Value.Quit();

            _driver.Value = null;

            _driver.Dispose();

            _driver = null;
        }
    }
}