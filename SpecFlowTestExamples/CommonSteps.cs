using System;
using TechTalk.SpecFlow;

namespace SpecFlowTestExamples
{
    [Binding]
    public class CommonSteps
    {
        [StepDefinition(@"Запущен браузер ""(.*)""")]
        public void ДопустимЗапущенБраузер(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [StepDefinition(@"пользователь \(заполняет поле\) ""(.*)"" значением ""(.*)""")]
        public void ЕслиПользовательЗаполняетПолеЗначением(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [StepDefinition(@"пользователь \(нажимает кнопку\) ""(.*)""")]
        public void ЕслиПользовательНажимаетКнопку(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [StepDefinition(@"появляется сообщение об ошибке с текстом ""(.*)""")]
        public void ТоПоявляетсяСообщениеОбОшибкеСТекстом(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
