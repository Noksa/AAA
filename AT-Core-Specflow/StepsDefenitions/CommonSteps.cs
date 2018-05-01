using AT_Core_Specflow.Core;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.StepsDefenitions
{
    [Binding]
    public class CommonSteps
    {
        #region Actions in pages

        [StepDefinition(@"открывается страница ""(.*)""")]
        public void OpenPage(string p0)
        {
            PageManager.PageContext.OpenPage(p0);
        }

        [StepDefinition("^пользователь \\((.*)\\)$")]
        public void ExecuteMethodByTitle(string actionTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle)
        {
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\" (?:значением|со значением) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle, string value)
        {
            //var button = (ImlButton)PageManager.PageContext.GetElementByTitle(elementTitle);
            //button.Click();
            PageManager.PageContext.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle, value);
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
