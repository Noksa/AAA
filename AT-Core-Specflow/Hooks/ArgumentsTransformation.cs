using System;
using System.Collections.Generic;
using System.Linq;
using AT_Core_Specflow.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class ArgumentsTransformation
    {
        private readonly ScenarioContext _scenarioContext;

        public ArgumentsTransformation(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [StepArgumentTransformation(".*из списка$")]
        public List<object> TableToList(Table table)
        {
            var list = new List<object>();
            if (table.Rows[0].Keys.Count > 1)
                throw new NullReferenceException("Table in this action cant have more then 1 column.");
            table.Rows.ToList().ForEach(row => { list.Add(ReplaceParamWithVariable(row.Values.First())); });
            return list;
        }

        [StepArgumentTransformation]
        public WrappedString WrapString(string str)
        {
            var writerAction = _scenarioContext.StepContext.StepInfo.Text;
            if (!writerAction.ToLower().Contains("(запоминает")) return new WrappedString(ReplaceParamWithVariable(str).ToString());
            var lastArg = _scenarioContext.StepContext.StepInfo.BindingMatch.Arguments.Last() as string;
            return lastArg != str ? new WrappedString(ReplaceParamWithVariable(str).ToString()) : new WrappedString(str);
        }

        private static object ReplaceParamWithVariable(object param)
        {
            var paramStr = param.ToString();
            if (!paramStr.StartsWith("~")) return param;
            var value = Stash.GetValueByKey(paramStr);
            if (value == null)
                throw new NullReferenceException(
                    $"Detected use variable with name '{param}' before it was written to Stash.");
            return value;
        }
    }


    public class WrappedString
    {
        public string Value { get; }

        public WrappedString(string value)
        {
            Value = value;
        }
    }
}
