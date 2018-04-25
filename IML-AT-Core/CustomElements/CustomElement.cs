using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Drawing;
using IML_AT_Core.Core;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace IML_AT_Core.CustomElements
{
    public class CustomElement : IWebElement
    {
        protected readonly IEnumerable<By> Bys;
        protected bool CacheLookup;
        protected string Title { get; set; }
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



        public CustomElement(IElementLocator locator, IEnumerable<By> bys, bool cache, string elementTitle)
        {
            Bys = bys;
            CacheLookup = cache;
            _locator = locator;
            Title = elementTitle;
        }

        public bool Displayed => WrappedElement.Displayed;
        public bool Enabled => WrappedElement.Enabled;
        public Point Location => WrappedElement.Location;
        public bool Selected => WrappedElement.Selected;
        public Size Size => WrappedElement.Size;
        public string TagName => WrappedElement.TagName;
        public string Text => WrappedElement.Text;

        public string NameOfElement
        {
            get
            {
                if (Title.Length != 0) return Title;
                var text = WrappedElement.GetAttribute("text");
                var value = WrappedElement.GetAttribute("value");
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
                return !string.IsNullOrEmpty(text) ? text : WrappedElement.ToString();
            }
        }

        public void Clear()
        {
            StepRunner.Run($"Очищаем элемент \"{NameOfElement}\"", () => WrappedElement.Clear());
        }

        public void Click()
        {
            StepRunner.Run($"Кликаем по элементу \"{NameOfElement}\"", () => WrappedElement.Click());
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
            StepRunner.Run($"Заполняем текст у элемента \"{NameOfElement}\"", () => WrappedElement.SendKeys(text));
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }
    }
}