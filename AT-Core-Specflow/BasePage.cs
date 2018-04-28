using IML_AT_Core.Core;
using IML_AT_Core.CustomElements.Elements;
using IML_AT_Core.Decorators;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace AT_Core_Specflow
{
    [Binding]
    public abstract class BasePage
    {
        protected BasePage()
        {
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ImlFieldDecorator());
        }

        [StepDefinition(@"пользователь нажимает кнопку ""(.*)""")]

        public void ЕслиПользовательНажимаетКнопку(string p0)

        {

            ScenarioContext.Current.Pending();

        }



        [StepDefinition(@"открывается страница ""(.*)""")]

        public void ТоОткрываетсяСтраница(string pageTitle)

        {

            CustomPageFactory.CurrentPage.Members.Clear();

            // CustomPageFactory.CurrentPage.OpenPage(pageTitle);

        }



        [StepDefinition(@"пользователь заполняет поле ""(.*)"" значением ""(.*)""")]

        public void ТоПользовательЗаполняетПолеЗначением(string p0, string p1)

        {

            ScenarioContext.Current.Pending();

        }



        [StepDefinition(@"пользователь \(нажимает кнопку\) ""(.*)""")]

        public void ТоПользовательНажимаетКнопку(string buttonName)

        {

            //  var btn = (ImlButton)CustomPageFactory.CurrentPage.GetElementByTitle(buttonName);

           // btn.Click();

        }



        [StepDefinition(@"Запускается браузер ""(.*)"" и открывается страница ""(.*)""")]

        public void ДопустимЗапускаетсяБраузерИОткрываетсяСтраница(string p0, string p1)

        {

            DriverFactory.GetDriver().Navigate().GoToUrl(p1);

        }



        [StepDefinition(@"пользователь \(заполняет поле\) ""(.*)"" значением ""(.*)""")]

        public void FillField(string elementTitle, string value)

        {

            // var field = (ImlTextInput)CustomPageFactory.Instance.GetElementByTitle(elementTitle);

            //  field.SendKeys(value);

        }

        [StepDefinition(@"пользователь \(проверяет значение элемента\) ""(.*)"" со значением ""(.*)""")]

        public void ТоПользовательПроверяетЗначениеЭлементаСоЗначением(string elementTitle, string expectedValue)

        {

            // var element = (IML_AT_Core.CustomElements.ImlElement)CustomPageFactory.Instance.GetElementByTitle(elementTitle);

            //  Assert.AreEqual(expectedValue, element.Text, "Значение элемента не совпадает с ожидаемым.");

        }









    }

}
