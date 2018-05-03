using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework.Internal;
using PageTitleAttribute = AT_Core_Specflow.CustomElements.Attributes.PageTitleAttribute;

namespace AT_Core_Specflow.Core
{
    public class PageManager
    {
        private static readonly ThreadLocal<HashSet<Type>> _PagesTypes = new ThreadLocal<HashSet<Type>>();
        private static readonly ThreadLocal<PageContext> CurrentPage = new ThreadLocal<PageContext>();

        public static PageContext PageContext
        {
            get
            {
                if (!CurrentPage.IsValueCreated) CurrentPage.Value = new PageContext();
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
            var pagesNamespace = ConfigurationManager.AppSettings.Get("PagesNamespace");
            if (string.IsNullOrEmpty(pagesNamespace)) throw new NUnitException($"Cant find \"PagesNamespace\" key value in app.config. Add it.");
            var allClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == pagesNamespace && t.GetCustomAttribute(typeof(PageTitleAttribute), true) != null).ToList();
            allClasses.ForEach(page => PagesTypes.Add(page));
        }
    }
}
