using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AT_Core_Specflow.Helpers
{
    public static class Stash
    {
        private static readonly ThreadLocal<Dictionary<string, object>> Dictionary =
            new ThreadLocal<Dictionary<string, object>>();

        public static Dictionary<string, object> AsDict
        {
            get
            {
                if (!Dictionary.IsValueCreated) Dictionary.Value = new Dictionary<string, object>();
            return Dictionary.Value;
            }
        }

        public static void Add(string key, string value)
        {
            if (AsDict.ContainsKey(key)) AsDict.Remove(key);
            AsDict.Add(key, value);
        }

        public static object GetValueByKey(string key)
        {
            return AsDict.FirstOrDefault(_ => _.Key == key).Value;
        }
    }
}


