using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using AT_Core_Specflow.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AT_Core_Specflow.CustomElements;
using AT_Core_Specflow.CustomElements.Attributes;

namespace AT_Core_Specflow.Decorators
{
    public abstract class BaseDecorator
    {
        protected static void AddElementToBlock(string blockName, object element, string elementTitle)
        {
            if (!PageManager.PageContext.ElementsInBlocks.TryGetValue(blockName, out var dictionary))
            {
                dictionary = new Dictionary<object, string>();
                PageManager.PageContext.ElementsInBlocks.Add(blockName, dictionary);
            }
            if (dictionary.ContainsKey(element))
            {
                dictionary[element] = elementTitle;
            }
            else
            {
                dictionary.Add(element, elementTitle);
            }
        }

        protected static ReadOnlyCollection<By> CreateLocatorList(MemberInfo member, Type targetType)
        {
            var bys = targetType.BaseType == typeof(ABlockElement) ? (targetType.GetCustomAttribute(typeof(FindByAttribute), true) as FindByAttribute)?.Bys : (member.GetCustomAttribute(typeof(FindByAttribute), true) as FindByAttribute)?.Bys;
            if (bys == null) throw new NullReferenceException($"Элемент \"{member.Name}\" не имеет аттрибут FindBy.\nДля поиска элемента добавьте аттрибут.");
            var useAll = bys.Count > 1;
            if (bys.Count == 0) return new List<By>().AsReadOnly();
            if (!useAll) return bys.AsReadOnly();
            var all = new ByAll(bys.ToArray());
            bys.Clear();
            bys.Add(all);
            return bys.AsReadOnly();
        }

        protected static bool ShouldCacheLookup(MemberInfo member)
        {
            var cacheAttributeType = typeof(CacheLookupAttribute);
            var cache = member.GetCustomAttributes(cacheAttributeType, true).Length != 0 ||
                        member.DeclaringType.GetCustomAttributes(cacheAttributeType, true).Length != 0;
            return cache;
        }

        protected static bool FieldNeedDecorated(MemberInfo member)
        {
            var baseType = member.DeclaringType?.BaseType;
            if (member.DeclaringType == typeof(ABlockElement) || member.DeclaringType == typeof(BasePage)) return false;
            return baseType == typeof(BasePage) || baseType == typeof(ABlockElement);
        }

        protected Type GetElementType(MemberInfo member)
        {
            switch (member)
            {
                case FieldInfo field:
                    return field.FieldType;
                case PropertyInfo property:
                    var hasPropertySet = property.CanWrite;
                    if (!hasPropertySet) return null;
                    return property.PropertyType;
                default:
                    return null;
            }
        }

        protected string GetElementTitle(MemberInfo member, Type targetType)
        {
            if (targetType.BaseType == typeof(ABlockElement) &&
                targetType.GetCustomAttribute(typeof(BlockTitleAttribute), true) is BlockTitleAttribute blockAttr)
                return blockAttr.Title;
            return ((ElementTitleAttribute) member.GetCustomAttribute(typeof(ElementTitleAttribute), true)).Name;
        }

        protected bool CheckElementType(Type targetType)
        {
            return targetType.BaseType == typeof(AElement) || targetType.BaseType == typeof(ABlockElement) ||
                   targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(AList<>);
        }
    }
}