using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Allure.Commons;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements.Attributes;
using AT_Core_Specflow.Extensions;
using AT_Core_Specflow.Helpers;
using TechTalk.SpecFlow;

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
            AllureLifecycle.Instance.UpdateFixture(_ => _.name = "Инициализация драйвера.");
            AddAllPagesToDictionary();
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
        
        private void AddAllPagesToDictionary()
        {
            var pages = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.BaseType == typeof(BasePage) && t.GetCustomAttribute(typeof(PageTitleAttribute), false) != null).ToList();
            pages.ForEach(page => PageManager.AllPages.Add(page));
        }
    }
}