using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class DriverWaitTypeChooser : IDriverWaitTypeChooser
    {
        private readonly TimeSpan _timespan;
        private readonly IWebDriver _driver;
        private WebDriverWait Wait => new WebDriverWait(_driver, _timespan);


        public DriverWaitTypeChooser(IWebDriver driver, TimeSpan timespan)
        {
            _timespan = timespan;
            _driver = driver;
        }



        public IPageWaitConditions UntilPage()

        {
            return new PageWaitConditions(_driver, _timespan);
        }



        public TResult Until<TResult>(Func<TResult> func)

        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed <= _timespan)
            {
                try
                {
                    return func();
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }

            }
            throw new WebDriverTimeoutException();

        }



        public TResult Until<TResult>(Func<IWebDriver, TResult> func)
        {
            return Wait.Until(drv => func(drv));
        }
    }

}

