using System;
using System.Configuration;
using System.Linq;

namespace AT_Core_Specflow.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class TimeToSearchAttribute : Attribute
    {
        public int TimeOut { get; }

        public TimeToSearchAttribute(string configKey)
        {
            TimeOut = SearchTimeoutInConfig(configKey);
        }

        public TimeToSearchAttribute(int timeOut)
        {
            TimeOut = timeOut;
        }

        private int SearchTimeoutInConfig(string prop)
        {
            var timing = ConfigurationManager.AppSettings.Get(prop);
            if (!string.IsNullOrEmpty(timing) && int.TryParse(timing, out var timeOut)) return timeOut;
            throw new NullReferenceException($"Cant find key '{prop}' in app.config.\nAdd it.");
        }
    }
}
