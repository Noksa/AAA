using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using PageTitleAttribute = AT_Core_Specflow.CustomElements.Attributes.PageTitleAttribute;

namespace AT_Core_Specflow.Core
{
    public class CustomPageFactory
    {
        private static readonly ThreadLocal<HashSet<Type>> _PagesTypes = new ThreadLocal<HashSet<Type>>();
        private static readonly ThreadLocal<PageWrapper> CurrentPage = new ThreadLocal<PageWrapper>();

        public static PageWrapper Instance
        {
            get
            {
                if (!CurrentPage.IsValueCreated) CurrentPage.Value = new PageWrapper();
                return CurrentPage.Value;
            }
        }

        public static HashSet<Type> PagesTypes
        {
            get
            {
                if (!_PagesTypes.IsValueCreated) _PagesTypes.Value = new HashSet<Type>();
                return _PagesTypes.Value;
            }
        }

        public static void AddAllPagesToList()
        {
            var allClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == "SpecFlowTests.Pages" && t.GetCustomAttribute(typeof(PageTitleAttribute), true) != null).ToList();
            allClasses.ForEach(page => PagesTypes.Add(page));
        }
    }
}
