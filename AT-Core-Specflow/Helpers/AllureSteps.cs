using System;
using Allure.Commons;

namespace AT_Core_Specflow.Helpers
{
    public abstract class AllureSteps
    {
        public static void AddSingleStep(string stepName)
        {
            var uuid = Guid.NewGuid().ToString();
            var step = new StepResult
            {
                name = stepName,
                status = Status.passed
            };
            AllureLifecycle.Instance.StartStep(uuid, step);
            AllureLifecycle.Instance.StopStep(uuid);
        }
    }
}
