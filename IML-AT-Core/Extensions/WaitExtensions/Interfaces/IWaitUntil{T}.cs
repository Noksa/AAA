using System;
using System.Linq.Expressions;

namespace IML_AT_Core.Extensions.WaitExtensions.Interfaces
{
    public interface IWaitUntil<T>
    {
        TResult Until<TResult>(Expression<Func<TResult>> func);
        TResult Until<TResult>(Expression<Func<T, TResult>> func);
    }
}