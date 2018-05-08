
using System;
using AT_Core_Specflow.Hooks;

namespace AT_Core_Specflow.Extensions
{
    public static class CommonExtension
    {
        public static void AddCommonVariablesToContext()
        {
            CoreSteps.ScenarioContext.Add("~Сегодня", DateTime.Now.ToString("dd.MM.yyyy"));
            CoreSteps.ScenarioContext.Add("~Завтра", DateTime.Now.AddDays(1).ToString("dd.MM.yyyy"));
        }
    }
}
