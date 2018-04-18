using System;
using System.Diagnostics;
using System.Threading;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public abstract class BaseWaitTypeChooser<T> : IWaitUntil<T>
    {
        protected TimeSpan TimeSpan { get; set; }
        protected T Obj { get; set; }
        protected BaseWaitTypeChooser(T obj, TimeSpan timespan = default(TimeSpan))
        {
            TimeSpan = timespan;
            Obj = obj;
        }

        public TResult Until<TResult>(Func<TResult> func)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed <= TimeSpan)
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

        public TResult Until<TResult>(Func<T, TResult> func)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed <= TimeSpan)
            {
                try
                {
                    return func(Obj);
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
    }
}
