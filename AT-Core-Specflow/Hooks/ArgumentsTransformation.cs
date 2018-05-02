using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT_Core_Specflow.Helpers;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class ArgumentsTransformation
    {
        [StepArgumentTransformation]
        public WrappedString WrapString(string str)
        {
            return new WrappedString(CheckParamForVariable(str));
        }

        private static string CheckParamForVariable(string str)
        {
            if (!str.StartsWith("~")) return str;
            var value = Stash.GetValueByKey(str);
            if (string.IsNullOrEmpty(value)) throw new NullReferenceException($"Detected variable with name '{str}' before it was written to Stash.");
            return value;
        }        
    }

    public class WrappedString
    {
        public WrappedString(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }
    }
}
