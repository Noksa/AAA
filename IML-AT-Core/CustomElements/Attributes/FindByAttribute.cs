using System;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace IML_AT_Core.CustomElements.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class FindByAttribute : Attribute
    {
        #region Fields
        private List<By> _bys;
        #endregion

        #region Properties
        public string Id
        {
            set => Add(By.Id(value));
            get => "";
        }
        public string Name
        {
            set => Add(By.Name(value));
            get => "";
        }
        public string ClassName
        {
            set => Add(By.ClassName(value));
            get => "";
        }
        public string Css
        {
            set => Add(By.CssSelector(value));
            get => "";
        }
        public string XPath
        {
            set => Add(By.XPath(value));
            get => "";
        }
        public string Tag
        {
            set => Add(By.TagName(value));
            get => "";
        }
        public string LinkText
        {
            set => Add(By.LinkText(value));
            get => "";
        }
        public string PartialLinkText
        {
            set => Add(By.PartialLinkText(value));
            get => "";
        }

        public List<By> Bys
        {
            get { return _bys ?? (_bys = new List<By>()); }
            #endregion
        }

        #region Private Methods
        private void Add(By value)
        {
            Bys.Add(value);
        }
        #endregion
    }
}