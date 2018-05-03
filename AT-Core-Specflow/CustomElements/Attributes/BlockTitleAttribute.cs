using System;

namespace AT_Core_Specflow.CustomElements.Attributes
{
    public class BlockTitleAttribute : Attribute
    {
        public string Title { get; }

        public BlockTitleAttribute(string title)
        {
            Title = title;
        }
    }
}
