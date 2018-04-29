using AT_Core_Specflow.Core;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow.StepsDefenitions
{
    [Binding]
    public class CommonSteps
    {
        [StepDefinition(@"открывается страница ""(.*)""")]
        public void OpenPage(string p0)
        {
            CustomPageFactory.Instance.OpenPage(p0);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle)
        {
            CustomPageFactory.Instance.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle);
        }

        [StepDefinition("^пользователь \\((.*)\\) \"([^\"]*)\" (?:значением|со значением) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitle(string actionTitle, string elementTitle, string value)
        {
            //var button = (ImlButton)CustomPageFactory.Instance.GetElementByTitle(elementTitle);
            //button.Click();
            CustomPageFactory.Instance.CurrentPage.ExecuteMethodByTitle(actionTitle, elementTitle, value);
        }


        // блоки

        [StepDefinition("^пользователь в блоке \"([^\"]*)\" \\((.*)\\) \"([^\"]*)\"$")]
        public void ExecuteMethodByTitleInBlock(string blockName, string actionTitle, string elementTitle)
        {
            CustomPageFactory.Instance.CurrentPage.ExecuteMethodByTitleInBlock(blockName, actionTitle, elementTitle);
        }

    }
}
