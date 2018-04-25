using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace IML_AT_Core.Core
{
    public static class TestRunner
    {
        private static readonly ThreadLocal<string> Uuid = new ThreadLocal<string>();

        public static void Run(Action stepBody)
        {
                var allureStepAttribute = (AllureStepAttribute)stepBody.Method.GetCustomAttributes(typeof(AllureStepAttribute), true).FirstOrDefault();
            if (allureStepAttribute != null)
            {
                Uuid.Value = Guid.NewGuid().ToString("N");
                var stepResult = new StepResult
                {
                    name = allureStepAttribute.StepDescription,
                    start = DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                AllureLifecycle.Instance.StartStep(Uuid.Value, stepResult);
                try
                {
                    stepBody();
                    AllureLifecycle.Instance.UpdateStep(Uuid.Value, result => { result.status = Status.passed; });
                }
                catch (Exception)
                {
                    var timestamp = DateTime.Now.ToString("dd-MM-yyyy-hhmmss");
                    var pathToFile = Path.Combine(TestContext.CurrentContext.TestDirectory,
                        TestContext.CurrentContext.Test.ID + "-" + timestamp + ".png");
                    DriverFactory.GetDriver().TakeScreenshot()
                        .SaveAsFile(pathToFile, ScreenshotImageFormat.Png);
                    var attachment = new Attachment
                    {
                        type = "image/png",
                        source = pathToFile
                    };
                    AllureLifecycle.Instance.UpdateStep(Uuid.Value,
                        result => { result.attachments.Add(attachment); });
                }
                finally
                {
                    AllureLifecycle.Instance.StopStep(Uuid.Value);
                }
            }
            else stepBody();
            
        }

        
    }
}


