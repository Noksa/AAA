using System;
using System.Diagnostics;
using System.Threading;
using System.Linq.Expressions;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class BaseWaitTypeChooser<T> : IWaitUntil<T>
    {
        public TimeSpan TimeSpan { get; set; }
        public T Obj { get; set; }

        public BaseWaitTypeChooser(T obj, TimeSpan timespan = default(TimeSpan))
        {
            TimeSpan = timespan;
            Obj = obj;
        }

        public TResult Until<TResult>(Expression<Func<TResult>> func)
        {
            return Until(_ => func.Compile()());
        }

        public TResult Until<TResult>(Expression<Func<T, TResult>> func)
        {
            var resultType = typeof(TResult);
            var stopwatch = new Stopwatch();
            var methodCall = func.Body as MethodCallExpression;
            stopwatch.Start();
            while (stopwatch.Elapsed <= TimeSpan)
            {
                try
                {
                    var result = func.Compile()(Obj);
                    if (resultType == typeof(bool))
                    {
                        var boolResult = result as bool?;
                        if (boolResult.HasValue && boolResult.Value) return result;
                    }
                    else return result;
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

            throw new WebDriverTimeoutException($"Timed out after {TimeSpan.TotalSeconds} seconds." +
                                                $"\n\nCaused in class: {methodCall.Method.DeclaringType.Name}" +
                                                $"\nMethodName: {methodCall.Method.Name}" +
                                                $"\nParameters: {string.Join(", ", methodCall.Arguments)}");
        }
    }
}