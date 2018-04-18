using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class ElementWaitTypeChooser : IElementWaitTypeChooser
    {
        private readonly IWebElement _element;
        private readonly TimeSpan _timespan;
        public ElementWaitTypeChooser(IWebElement element, TimeSpan timespan)
        {
            _element = element;
            _timespan = timespan;
        }
    }
}
