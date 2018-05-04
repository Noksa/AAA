using System;
using System.Linq.Expressions;

namespace AT_Core_Specflow.Extensions.WaitExtensions.Interfaces
{
    public interface IWaitUntil<T>
    {
        TResult Until<TResult>(Expression<Func<TResult>> func);
        TResult Until<TResult>(Expression<Func<T, TResult>> func);
    }
}