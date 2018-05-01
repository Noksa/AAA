using System;

namespace AT_Core_Specflow.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ActionTitleAttribute : Attribute
    {
        public string ActionTitle { get; set; }
        public ActionTitleAttribute(string actionTitle)
        {
            ActionTitle = actionTitle;
        }
    }
}
