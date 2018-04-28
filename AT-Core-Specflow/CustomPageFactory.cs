using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AT_Core_Specflow
{
    public class CustomPageFactory
    {
        private static readonly ThreadLocal<HashSet<Type>> _pagesTypes = new ThreadLocal<HashSet<Type>>();
        private static readonly ThreadLocal<PageWrapper> _currentPage = new ThreadLocal<PageWrapper>();

        public static PageWrapper CurrentPage
        {
            get
            {
                if (!_currentPage.IsValueCreated) _currentPage.Value = new PageWrapper();
                return _currentPage.Value;
            }
        }

        public static HashSet<Type> PagesTypes
        {
            get
            {
                if (!_pagesTypes.IsValueCreated) _pagesTypes.Value = new HashSet<Type>();
                return _pagesTypes.Value;
            }
        }
    }
}
