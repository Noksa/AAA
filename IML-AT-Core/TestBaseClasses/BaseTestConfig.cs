﻿using System;
using System.Configuration;
using IML_AT_Core.Core;
using IML_AT_Core.Helpers;
using NUnit.Framework;

namespace IML_AT_Core.TestBaseClasses
{
    public abstract class BaseTestConfig
    {
        public TestContext TestContext => TestContext.CurrentContext;
        protected Browser Browser;
        protected string Url;

        [SetUp]
        public virtual void Setup()
        {
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

        [TearDown]
        public virtual void TearDown()
        {
            DriverFactory.Dispose();
        }
    }
}