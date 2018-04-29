using System;
using System.Collections.Generic;
using System.Reflection;
using PageTitleAttribute = AT_Core_Specflow.CustomElements.Attributes.PageTitleAttribute;

namespace AT_Core_Specflow.Core
{
    public class PageWrapper
    {
        private Dictionary<object, string> _members;
        public Dictionary<object, string> Members => _members ?? (_members = new Dictionary<object, string>());

        public BasePage CurrentPage { get; private set; }

        public BasePage OpenPage(string title)
        {
            foreach (var page in CustomPageFactory.PagesTypes)
            {
                if (!(page.GetCustomAttribute(typeof(PageTitleAttribute)) is PageTitleAttribute attr) ||
                    attr.Title != title) continue;
                var newPage = (BasePage)Activator.CreateInstance(page);
                CurrentPage = newPage;
                return CurrentPage;
            }
            throw new NullReferenceException($"Cant initialize page with title '{title}'.");
        }

    }
}
