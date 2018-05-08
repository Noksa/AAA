using System;

namespace AT_Core_Specflow.Extensions.WaitExtensions.Interfaces
{
    public interface IWaitUntil<out T>
    {
        TResult Until<TResult>(Func<TResult> func);
        TResult Until<TResult>(Func<T, TResult> func);
    }
}