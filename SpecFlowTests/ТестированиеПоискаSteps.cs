using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class ТестированиеПоискаSteps
    {
        [When(@"пользователь попадает на главную страницу")]
        public void ЕслиПользовательПопадаетНаГлавнуюСтраницу()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"пользователь ищет ""(.*)""")]
        public void ЕслиПользовательИщет(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"пользователь попадает на страницу результатов поиска")]
        public void ТоПользовательПопадаетНаСтраницуРезультатовПоиска()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"пользователь проверяет что в результатах есть ""(.*)"" с минимальной стоимостью ""(.*)""")]
        public void ТоПользовательПроверяетЧтоВРезультатахЕстьСМинимальнойСтоимостью(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
