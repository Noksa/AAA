using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using AT_Core_Specflow.CustomElements.Attributes;

namespace AT_Core_Specflow.Core
{
    public class PageManager
    {
        private static readonly ThreadLocal<HashSet<Type>> PagesTypes = new ThreadLocal<HashSet<Type>>();
        private static readonly ThreadLocal<PageContext> CurrentPage = new ThreadLocal<PageContext>();

        public static PageContext PageContext
        {
            get
            {
                if (!CurrentPage.IsValueCreated) CurrentPage.Value = new PageContext();
                return CurrentPage.Value;
            }
        }

        public static HashSet<Type> AllPages
        {
            get
            {
                if (!PagesTypes.IsValueCreated) PagesTypes.Value = new HashSet<Type>();
                return PagesTypes.Value;
            }
        }
    }
}
