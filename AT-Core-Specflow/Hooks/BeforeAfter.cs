using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Allure.Commons;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.Extensions;
using AT_Core_Specflow.Helpers;
using IML_AT_Core.Core;
using NUnit.Framework;
using TechTalk.SpecFlow;
using DriverFactory = AT_Core_Specflow.Core.DriverFactory;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class BeforeAfter
    {
        protected Browser Browser;
        protected string Url;

        /// <summary>
        /// Delete old allure results before all tests are executed.
        /// </summary>
        [BeforeTestRun]        
        public static void DeleteOldResults()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [BeforeScenario]
        public virtual void Setup()
        {
            PageManager.AddAllPagesToList();
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
            AllureLifecycle.Instance.UpdateFixture(_ => _.name = "Инициализация драйвера.");
        }

        [AfterScenario]
        public virtual void TearDown()
        {
            DriverFactory.Dispose();
            AllureLifecycle.Instance.UpdateFixture(_ => _.name = "Переменные теста");
            AddTestVariablesToReport();
        }

        private void AddTestVariablesToReport()
        {
            var list = new List<StepResult>();
            CoreSteps.ScenarioContext.ToList().Where(_ => _.Key.StartsWith("~")).ToList().ForEach(_ =>
            {
                var step = new StepResult
                {
                    name = $"Переменная \"{_.Key}\", значение: \"{_.Value}\"",
                    status = Status.passed
                };
                list.Add(step);
            });
            
            list.ForEach(_ => AllureSteps.AddSingleStep(_.name));
        }
    }
}