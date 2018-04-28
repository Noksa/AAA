using System;
using System.Configuration;
using IML_AT_Core.Core;
using IML_AT_Core.Helpers;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow
{
    [Binding]
    public class BaseTestConfig
    {
        protected Browser Browser;
        protected string Url;

        [BeforeScenario]
        public virtual void Setup()
        {
            CustomPageFactory.AddAllPagesToList();
            var parsed = Enum.TryParse(ConfigurationManager.AppSettings.Get("BrowserType").FirstCharToUpperAndOtherToLower(), out Browser);
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

        [AfterScenario]
        public virtual void TearDown()
        {
            DriverFactory.Dispose();
        }
    }
}