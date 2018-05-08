using System;

namespace AT_Core_Specflow.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageTitleAttribute : Attribute
    {
        public PageTitleAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; }
    }
}