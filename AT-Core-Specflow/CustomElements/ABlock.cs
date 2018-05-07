using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.Decorators;
using AT_Core_Specflow.Extensions.WaitExtensions;
using AT_Core_Specflow.Extensions.WaitExtensions.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements
{
    public abstract class ABlock : BasePage, IWebElement
    {
        protected ABlock(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle)
        {
            Bys = bys;
            _locator = locator;
            CacheLookup = cache;
            Title = elementTitle;
            PageFactory.InitElements(DriverFactory.GetDriver(), this, new ABlockDecorator());
        }

        protected readonly IEnumerable<By> Bys;
        protected bool CacheLookup;
        public string Title { get; }
        public int TimeOut { get; set; }
        private readonly IElementLocator _locator;
        private IWebElement _realElement;
        

        private IWebElement WrappedElement
        {
            get
            {
                if (!CacheLookup || WrappedElement == null) _realElement = _locator.LocateElement(Bys);
                return _realElement;
            }
        }

        public bool Displayed => WrappedElement.Displayed;
        public bool Enabled => WrappedElement.Enabled;
        public Point Location => WrappedElement.Location;
        public bool Selected => WrappedElement.Selected;
        public Size Size => WrappedElement.Size;
        public string TagName => WrappedElement.TagName;
        public string Text => WrappedElement.Text;

        public void Clear()
        {
            WrappedElement.Clear();
        }

        public void Click()
        {
            WrappedElement.Click();
        }

        public IWebElement FindElement(By by)
        {
            return WrappedElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WrappedElement.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return WrappedElement.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return WrappedElement.GetProperty(propertyName);
        }

        public void SendKeys(string text)
        {
            WrappedElement.SendKeys(text);
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }

        public IWaitUntil<ABlock> Wait(TimeSpan timespan = default(TimeSpan))
        {
            if (timespan == default(TimeSpan)) timespan = TimeSpan.FromSeconds(5);
            return new BaseWaitTypeChooser<ABlock>(this, timespan);
        }
    }
}

