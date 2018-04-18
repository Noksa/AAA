using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public static class WaitExtension
    {
        public static IElementWaitTypeChooser Wait(this IWebElement element, TimeSpan timespan = default (TimeSpan))
        {
            return new ElementWaitTypeChooser(element, timespan);
        }



        public static IDriverWaitTypeChooser Wait(this IWebDriver driver, TimeSpan timespan = default (TimeSpan))
        {
            return new DriverWaitTypeChooser(driver, timespan);
        }
    }
}




