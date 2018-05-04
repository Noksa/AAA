using System;
using System.Threading;
using AT_Core_Specflow.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace AT_Core_Specflow.Extensions.WaitExtensions

{
    public static class WaitExtension
    {
        internal static ThreadLocal<TimeSpan> Timespan = new ThreadLocal<TimeSpan>();

        public static IWaitUntil<IWebElement> Wait(this IWebElement element, TimeSpan timespan = default(TimeSpan))
        {
            Timespan.Value = timespan;
            return new BaseWaitTypeChooser<IWebElement>(element, timespan);
        }

        public static IWaitUntil<IWebDriver> Wait(this IWebDriver driver, TimeSpan timespan = default(TimeSpan))
        {
            Timespan.Value = timespan;
            return new BaseWaitTypeChooser<IWebDriver>(driver, timespan);
        }
    }

    public static class ElementWaitExtension
    {
        public static bool Exists(this IWebElement element)
        {
            return element.Displayed;
        }
    }


    public static class DriverWaitExtension
    {
        public static IPageWaitConditions Page(this IWebDriver driver)
        {
            return new PageWaitConditions(driver, WaitExtension.Timespan.Value);
        }
    }
}