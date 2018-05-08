using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(".* из списка$")]
        public List<object> TableToObjectList(Table table)
        {
            var list = new List<object>();
            TransformHelpFunc.CheckTableIsList(table);
            table.Rows.ToList().ForEach(row => list.Add(ReplaceParamWithVariable(row.Values.First())));
            return list;
        }

        [StepArgumentTransformation(".* из таблицы$")]
        public Dictionary<string, string> TableToDictionary(Table table)
        {
            TransformHelpFunc.CheckTableIsDictionary(table);
            var dictionary = table.Rows.ToDictionary(row => ReplaceParamWithVariable(row[0]).ToString(), row => ReplaceParamWithVariable(row[1]).ToString());
            return dictionary;
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
            if (paramStr == "$Пробел") return " ";
            if (!paramStr.StartsWith("~")) return param;
            try
            {
                value = CoreSteps.ScenarioContext.Get<object>(paramStr);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(
                    $"Обнаружено использование переменной '{param}' прежде чем её значение было записано в хранилище.\nПеред использованием переменной необходимо записать её в хранилище.");
            }
            return value;
        }



        private abstract class TransformHelpFunc
        {
            public static void CheckTableIsList(Table table)
            {
                if (table.Rows[0].Keys.Count > 1)
                    throw new NullReferenceException($"Таблица в действии \"{CoreSteps.ScenarioContext.StepContext.StepInfo.Text}\" не может иметь больше 1 столбца.");
            }

            public static void CheckTableIsDictionary(Table table)
            {
                if (table.Rows[0].Values.Count != 2) throw new NullReferenceException($"Таблица в действии \"{CoreSteps.ScenarioContext.StepContext.StepInfo.Text}\" должна иметь 2 столбца.");
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
