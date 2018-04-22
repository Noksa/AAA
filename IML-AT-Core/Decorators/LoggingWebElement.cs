using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace IML_AT_Core.Decorators
{
    public class LoggingWebElement : IWebElement
    {
        private readonly IWebElement _webElementImplementation;

        public LoggingWebElement(IWebElement webElementImplementation)
        {
            _webElementImplementation = webElementImplementation;
        }

        public IWebElement FindElement(By by)
        {
            return _webElementImplementation.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _webElementImplementation.FindElements(by);
        }

        public void Clear()
        { TestContext.WriteLine($"Очищаем элемент {_webElementImplementation}");
            _webElementImplementation.Clear();
        }

        public void SendKeys(string text)
        {
            _webElementImplementation.SendKeys(text);
        }

        public void Submit()
        {
            _webElementImplementation.Submit();
        }

        public void Click()
        {
            _webElementImplementation.Click();
        }

        public string GetAttribute(string attributeName)
        {
            return _webElementImplementation.GetAttribute(attributeName);
        }

        public string GetProperty(string propertyName)
        {
            return _webElementImplementation.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return _webElementImplementation.GetCssValue(propertyName);
        }

        public string TagName => _webElementImplementation.TagName;

        public string Text => _webElementImplementation.Text;

        public bool Enabled => _webElementImplementation.Enabled;

        public bool Selected => _webElementImplementation.Selected;

        public Point Location => _webElementImplementation.Location;

        public Size Size => _webElementImplementation.Size;

        public bool Displayed => _webElementImplementation.Displayed;
    }
}
