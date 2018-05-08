using System;

namespace AT_Core_Specflow.Extensions.WaitExtensions
{
    public interface IWaitUntil<T>
    {
        TResult Until<TResult>(Func<TResult> action);
        TResult Until<TResult>(Func<T, TResult> action);
    }
}
