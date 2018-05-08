using System;
using System.Configuration;
using System.Threading;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace AT_Core_Specflow.Extensions.WaitExtensions

{
    public static class WaitExtension
    {
        private static readonly ThreadLocal<TimeSpan> Timespan = new ThreadLocal<TimeSpan>();

        public static TimeSpan Timing
        {
            get => Timespan.Value;
            set => Timespan.Value = value;
        }

        public static IWaitUntil<T> Wait<T>(this T obj, TimeSpan timespan = default(TimeSpan))
        {
            if (timespan == default(TimeSpan))
            {
               if (obj is IWebDriver) timespan = TimeSpan.FromSeconds(10);
               else if (obj.GetType().BaseType == typeof(AProxyElement))
               {
                   var aProxyElement = obj as AProxyElement;
                   timespan = TimeSpan.FromSeconds(aProxyElement.TimeOut);
               }
               else if (obj.GetType().BaseType == typeof(ABlock))
                {
                    var aBlock = obj as ABlock;
                    timespan = TimeSpan.FromSeconds(aBlock.TimeOut);
                }
            }
            Timing = timespan;
            return new BaseWaitTypeChooser<T>(obj, timespan);
        }
    }
}