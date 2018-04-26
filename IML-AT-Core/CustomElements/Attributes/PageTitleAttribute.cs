using System;

namespace IML_AT_Core.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageTitleAttribute : Attribute
    {
        public PageTitleAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
    }
}