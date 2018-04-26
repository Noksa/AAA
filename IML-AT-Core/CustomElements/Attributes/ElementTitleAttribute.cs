using System;

namespace IML_AT_Core.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class ElementTitleAttribute : Attribute
    {
        public ElementTitleAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

}
