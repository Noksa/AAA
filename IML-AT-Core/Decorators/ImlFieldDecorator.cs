using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.PageObjects;
using System.Reflection;
using System.Collections.Generic;
using IML_AT_Core.CustomElements;
using IML_AT_Core.CustomElements.Attributes;

namespace IML_AT_Core.Decorators
{
    public class ImlFieldDecorator : IPageObjectMemberDecorator
    {
        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            var elementTitle = "";
            Type targetType;
            var cache = ShouldCacheLookup(member);

            switch (member)
            {
                case FieldInfo field:
                    targetType = field.FieldType;
                    break;
                case PropertyInfo property:
                    var hasPropertySet = property.CanWrite;
                    targetType = property.PropertyType;
                    if (!hasPropertySet) return null;
                    break;
                default:
                    return null;
            }
            var elementTitleAttribute = targetType.BaseType == typeof(ImlBlockElement) ? targetType.GetCustomAttribute(typeof(ElementTitleAttribute), true) as ElementTitleAttribute : member.GetCustomAttribute(typeof(ElementTitleAttribute), true) as ElementTitleAttribute;
            if (elementTitleAttribute != null && elementTitleAttribute.Name.Length > 0)
                elementTitle = elementTitleAttribute.Name;

            IList<By> bys = CreateLocatorList(member, targetType);
            if (bys.Count <= 0) return null;
            if (targetType.BaseType == typeof(ImlElement) || targetType.BaseType == typeof(ImlBlockElement) ||
                targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(ImlList<>))
                return Activator.CreateInstance(targetType, locator, bys, cache, elementTitle);
            throw new NotImplementedException(
                $"Класс элемента \"{member.DeclaringType}.{targetType.Name}\" не является классом, который может быть декорирован.\nДекорирование возможно для класса \"ImlList<>\", а так же для наследников классов \"ImlBlockElement\" и \"ImlElement\"");
        }

        private static ReadOnlyCollection<By> CreateLocatorList(MemberInfo member, Type targetType)
        {
            var bys = targetType.BaseType == typeof(ImlBlockElement) ? (targetType.GetCustomAttribute(typeof(FindByAttribute), true) as FindByAttribute)?.Bys : (member.GetCustomAttribute(typeof(FindByAttribute), true) as FindByAttribute)?.Bys;
            if (bys == null) throw new NullReferenceException($"Элемент \"{member.Name}\" не имеет аттрибут FindBy.\nДля поиска элемента добавьте аттрибут.");
            var useAll = bys.Count > 1;
            if (bys.Count == 0) return new List<By>().AsReadOnly();
            if (!useAll) return bys.AsReadOnly();
            var all = new ByAll(bys.ToArray());
            bys.Clear();
            bys.Add(all);
            return bys.AsReadOnly();
        }

        private static bool ShouldCacheLookup(MemberInfo member)
        {
            var cacheAttributeType = typeof(CacheLookupAttribute);
            var cache = member.GetCustomAttributes(cacheAttributeType, true).Length != 0 ||
                        member.DeclaringType.GetCustomAttributes(cacheAttributeType, true).Length != 0;
            return cache;
        }

        
    }
}