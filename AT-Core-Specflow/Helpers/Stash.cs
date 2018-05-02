using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AT_Core_Specflow.Helpers
{
    public static class Stash
    {
        private static readonly ThreadLocal<Dictionary<string, string>> Dictionary =
            new ThreadLocal<Dictionary<string, string>>();

        public static Dictionary<string, string> AsDict
        {
            get
            {
                if (!Dictionary.IsValueCreated) Dictionary.Value = new Dictionary<string, string>();
            return Dictionary.Value;
            }
        }

        public static void Add(string key, string value)
        {
            AsDict.Add(key, value);
        }

        public static string GetValueByKey(string key)
        {
            return AsDict.FirstOrDefault(_ => _.Key == key).Value;
        }
    }
}


