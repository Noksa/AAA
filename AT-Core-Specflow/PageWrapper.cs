using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IML_AT_Core.CustomElements.Attributes;
using PageTitleAttribute = AT_Core_Specflow.CustomElements.Attributes.PageTitleAttribute;

namespace AT_Core_Specflow
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
                if (page.GetCustomAttribute(typeof(PageTitleAttribute)) is PageTitleAttribute attr &&
                    attr.Title == title)
                {
                    var newPage = (BasePage)Activator.CreateInstance(page);
                    CurrentPage = newPage;
                    return CurrentPage;
                }
            }

            throw new NullReferenceException($"Cant initialize page with title '{title}'.");
        }


        public object GetElementByTitle(string elementTitle)
        {
            foreach (var member in Members)
            {
                if (member.Value == elementTitle) return member.Key;
            }
            throw new NullReferenceException($"Cant find element with title '{elementTitle}' in page {CurrentPage.GetType()}");
        }
    }
}
