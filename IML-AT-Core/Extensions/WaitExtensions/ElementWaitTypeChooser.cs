using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class ElementWaitTypeChooser : BaseWaitTypeChooser<IWebElement>, IElementWaitTypeChooser
    {
        public ElementWaitTypeChooser(IWebElement element, TimeSpan timespan) : base(element, timespan)
        {
            
        }
    }
}
