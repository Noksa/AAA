using OpenQA.Selenium;

namespace AT_Core_Specflow.Extensions
{
    public static class ElementExtensions
    {
        public static string GetValue(this IWebElement element)
        {
            string value;
            switch (element.TagName)
            {
                case "input":
                    value = element.GetAttribute("value");
                    break;
                case "select":
                    value = element.GetAttribute("title");
                    if (string.IsNullOrEmpty(value)) value = element.Text;
                    break;
                default:
                    value = element.Text;
                    break;
            }

            return value;
        }
    }
}
