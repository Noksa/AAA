using AT_Core_Specflow.Core;
using AT_Core_Specflow.Hooks;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.StepsDefenitions
{
    [Binding]
    public class CommonSteps
    {
        #region Actions in pages

        [StepDefinition(@"открывается страница ""(.*)""")]
        public void OpenPage(WrappedString pageTitle)
        {
            PageManager.PageContext.OpenPage(pageTitle.Value);
        }

        [StepDefinition("^пользователь \\((.*)\\)$")]
        public void ExecuteMethodByTitle(string actionTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, WrappedString elementTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle.Value);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\" (?:значением|со значением) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, WrappedString elementTitle, WrappedString value)
        {
            //var button = (ImlButton)PageManager.PageContext.GetElementByTitle(elementTitle);
            //button.Click();
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle.Value, value.Value);
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
