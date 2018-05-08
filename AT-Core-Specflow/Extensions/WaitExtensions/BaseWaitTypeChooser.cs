using System;
using System.Diagnostics;
using System.Threading;
using AT_Core_Specflow.Extensions.WaitExtensions.Interfaces;

namespace AT_Core_Specflow.Extensions.WaitExtensions
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

        public TResult Until<TResult>(Func<TResult> func)
        {
            return Until(_ => func.Invoke());
        }

        public TResult Until<TResult>(Func<T, TResult> func)
        {
            var resultType = typeof(TResult);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = default(TResult);
            while (stopwatch.Elapsed <= TimeSpan)
            {
                try
                {
                    result = func.Invoke(Obj);
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
            return result;
        }
    }
}