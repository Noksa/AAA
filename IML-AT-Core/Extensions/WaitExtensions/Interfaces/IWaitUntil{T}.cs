using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IML_AT_Core.Extensions.WaitExtensions.Interfaces
{
    public interface IWaitUntil<out T>
    {
        TResult Until<TResult>(Func<TResult> func);
        TResult Until<TResult>(Func<T, TResult> func);
    }
}
