using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(".*из списка$")]
        public List<object> TableToObjectList(Table table)
        {
            var list = new List<object>();
            TransformHelpFunc.CheckTableIsList(table);
            table.Rows.ToList().ForEach(row => list.Add(ReplaceParamWithVariable(row.Values.First())));
            return list;
        }

        [StepArgumentTransformation]
        public WrappedString WrapString(string str)
        {
            var writerAction = CoreSteps.ScenarioContext.StepContext.StepInfo.Text;
            if (!writerAction.ToLower().Contains("(запоминает"))
                return new WrappedString(ReplaceParamWithVariable(str).ToString());
            var lastArg = CoreSteps.ScenarioContext.StepContext.StepInfo.BindingMatch.Arguments.Last() as string;
            return lastArg != str
                ? new WrappedString(ReplaceParamWithVariable(str).ToString())
                : new WrappedString(str);
        }

        private static object ReplaceParamWithVariable(object param)
        {
            object value;
            var paramStr = param.ToString();
            if (!paramStr.StartsWith("~")) return param;
            try
            {
                value = CoreSteps.ScenarioContext.Get<object>(paramStr);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(
                    $"Detected using variable with name '{param}' before it was written to stah.\nWrite variable to stash firstly.");
            }
            return value;
        }



        private abstract class TransformHelpFunc
        {
            public static void CheckTableIsList(Table table)
            {
                if (table.Rows[0].Keys.Count > 1)
                    throw new NullReferenceException($"Table in action \"{CoreSteps.ScenarioContext.StepContext.StepInfo.Text}\" cant have more then 1 column.");
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
}
