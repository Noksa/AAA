using System.Collections.Generic;
using System.Threading;
using AT_Core_Specflow.Core;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.Hooks
{
    [Binding]
    public class CoreSteps
    {
        private static readonly ThreadLocal<ScenarioContext> ScenarioContextThreadLocal = new ThreadLocal<ScenarioContext>();
        public static ScenarioContext ScenarioContext => ScenarioContextThreadLocal.Value;

        public CoreSteps(ScenarioContext scenarioContext)
        {
            ScenarioContextThreadLocal.Value = scenarioContext;
        }

        #region Actions in pages

        [StepDefinition(@"открывается страница ""(.*)""")]
        public void OpenPage(string pageTitle)
        {
            PageManager.PageContext.OpenPage(pageTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\)$")]
        public void ExecuteMethodByTitle(string actionTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string param1)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, param1);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\" (?:значением |со значением | |)\"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, Transforms.WrappedString param1, Transforms.WrappedString param2)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, param1.Value, param2.Value);
        }

        [StepDefinition("^пользователь \\((.*)\\) из списка$")]
        public void ExecuteMethodByTitle(string actionTitle, List<object> list)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, list);
        }

        #endregion

        #region Actions in blocks

        [StepDefinition("^пользователь в блоке \"([^\"]*)\" \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle, string elementTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitleInBlock(blockName, actionTitle, elementTitle);
        }

        [StepDefinition("^пользователь в блоке \"([^\"]*)\" \\((.*)\\)$")]
        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitleInBlock(blockName, actionTitle);
        }

        #endregion

    }
}
