using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IML_AT_Core.Core;
using IML_AT_Core.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IML_AT_Core.Extensions.WaitExtensions
{
    public class DriverWaitTypeChooser : BaseWaitTypeChooser<IWebDriver>, IDriverWaitTypeChooser
    {
        public DriverWaitTypeChooser(IWebDriver driver, TimeSpan timespan) : base(driver, timespan)
        {
        }

        public IPageWaitConditions UntilPage()
        {
            return new PageWaitConditions(Obj, TimeSpan);
        }
    }
}



