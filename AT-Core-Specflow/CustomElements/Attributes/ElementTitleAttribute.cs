using System;

namespace AT_Core_Specflow.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class ElementTitleAttribute : Attribute
    {
        public ElementTitleAttribute(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }

}
