using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsExamples
{
    public static class Pages<T>
    {
        public static TPage GetPage<TPage>(TPage page) where TPage: new ()
        {
            return page;
        }
    }
}
