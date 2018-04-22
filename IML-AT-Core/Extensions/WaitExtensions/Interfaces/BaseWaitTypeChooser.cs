using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IML_AT_Core.Extensions.WaitExtensions.Interfaces
{
    public class BaseWaitTypeChooser<T> : IWaitUntil<T>
    {
        public TResult Until<TResult>(Func<TResult> func)
        {
            throw new NotImplementedException();
        }

        public TResult Until<TResult>(Func<T, TResult> func)
        {
            throw new NotImplementedException();
        }
    }
}
