using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT_Core_Specflow.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

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
        [StepArgumentTransformation]
        public WrappedString WrapString(string param)
        {
            return new WrappedString(CheckParamForVariable(param));
        }

        private string CheckParamForVariable(string param)
        {
            var actionText = _scenarioContext.StepContext.StepInfo.Text;
            if (!actionText.ToLower().Contains("(запоминает")) return ReplaceParamWithVariable(param);
            var lastArg = _scenarioContext.StepContext.StepInfo.BindingMatch.Arguments.LastOrDefault() as string;
            return param != lastArg ? ReplaceParamWithVariable(param) : param;
        }

        private string ReplaceParamWithVariable(string param)
        {
            if (!param.StartsWith("~")) return param;
            var value = Stash.GetValueByKey(param);
            if (string.IsNullOrEmpty(value)) throw new NullReferenceException($"Detected use variable with name '{param}' before it was written to Stash.");
            return value;
        }
    }

    public class WrappedString
    {
        public WrappedString(string value)
        {
            Value = value;
        }
        public string Value { get; }
    }
}
