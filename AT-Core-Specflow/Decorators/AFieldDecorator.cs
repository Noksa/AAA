using System;
using System.Reflection;
using AT_Core_Specflow.Core;
using AT_Core_Specflow.CustomElements;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.Decorators
{
    public class AFieldDecorator : BaseDecorator, IPageObjectMemberDecorator
    {
        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            if (!FieldNeedDecorated(member)) return null;
            var cache = ShouldCacheLookup(member);
            var targetType = GetElementType(member);
            var elementTitle = GetElementTitle(member, targetType);
            var bys = CreateLocatorList(member, targetType);
            if (bys.Count <= 0) return null;

            if (CheckElementType(targetType))
            {
                var element = Activator.CreateInstance(targetType, locator, bys, cache, elementTitle);
                PageManager.PageContext.Elements.Add(element, elementTitle);
                SetTimeOutSearch(member, targetType, element);
                return element;
            }
            throw new NotImplementedException(
                $"Класс элемента \"{member.DeclaringType}.{targetType.Name}\" не является классом, который может быть декорирован.\nДекорирование возможно для класса \"AList<>\", а так же для наследников классов \"ABlock\" и \"AProxyElement\"");
        }
    }
}

