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
    public class ExtendedFieldDecorator : IPageObjectMemberDecorator
    {
        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            var elementTitle = "";
            var elementTitleAttribute =
                (ElementTitleAttribute) Attribute.GetCustomAttribute(member, typeof(ElementTitleAttribute));
            if (elementTitleAttribute != null && elementTitleAttribute.Name.Length != 0)
            {
                elementTitle = elementTitleAttribute.Name;
            }
            var field = member as FieldInfo;
            var property = member as PropertyInfo;

            Type targetType = null;
            if (field != null)
            {
                targetType = field.FieldType;
            }

            var hasPropertySet = false;
            if (property != null)
            {
                hasPropertySet = property.CanWrite;
                targetType = property.PropertyType;
            }

            if (field == null & (property == null || !hasPropertySet))
            {
                return null;
            }

            if (!(targetType.BaseType == typeof(CustomElement)))
            {
                throw new NotImplementedException(
                    $"Элемент {member.Name} не наследует класс CustomElement, и не может быть декорирован по этой причине.");
            }

            IList<By> bys = CreateLocatorList(member);
            if (bys.Count <= 0) return null;
            var cache = ShouldCacheLookup(member);
            var wrapper = Activator.CreateInstance(targetType, locator, bys, cache, elementTitle);
            return wrapper;
        }

        protected static ReadOnlyCollection<By> CreateLocatorList(MemberInfo member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "memeber cannot be null");
            }

            var useSequenceAttributes = Attribute.GetCustomAttributes(member, typeof(FindsBySequenceAttribute), true);
            var useSequence = useSequenceAttributes.Length > 0;

            var useFindAllAttributes = Attribute.GetCustomAttributes(member, typeof(FindsByAllAttribute), true);
            var useAll = useFindAllAttributes.Length > 0;

            if (useSequence && useAll)
            {
                throw new ArgumentException("Cannot specify FindsBySequence and FindsByAll on the same member");
            }

            var bys = new List<By>();
            var attributes = Attribute.GetCustomAttributes(member, typeof(FindsByAttribute), true);
            if (attributes.Length <= 0) return bys.AsReadOnly();
            Array.Sort(attributes);
            foreach (var attribute in attributes)
            {
                var castedAttribute = (FindsByAttribute) attribute;
                if (castedAttribute.Using == null)
                {
                    castedAttribute.Using = member.Name;
                }

                bys.Add(From(castedAttribute));
            }

            if (useSequence)
            {
                ByChained chained = new ByChained(bys.ToArray());
                bys.Clear();
                bys.Add(chained);
            }

            if (useAll)
            {
                var all = new ByAll(bys.ToArray());
                bys.Clear();
                bys.Add(all);
            }

            return bys.AsReadOnly();
        }

        private static By From(FindsByAttribute attribute)
        {
            var how = attribute.How;
            var usingValue = attribute.Using;
            switch (how)
            {
                case How.Id:
                    return By.Id(usingValue);
                case How.Name:
                    return By.Name(usingValue);
                case How.TagName:
                    return By.TagName(usingValue);
                case How.ClassName:
                    return By.ClassName(usingValue);
                case How.CssSelector:
                    return By.CssSelector(usingValue);
                case How.LinkText:
                    return By.LinkText(usingValue);
                case How.PartialLinkText:
                    return By.PartialLinkText(usingValue);
                case How.XPath:
                    return By.XPath(usingValue);
                case How.Custom:
                    if (attribute.CustomFinderType == null)
                    {
                        throw new ArgumentException("Cannot use How.Custom without supplying a custom finder type");
                    }

                    if (!attribute.CustomFinderType.IsSubclassOf(typeof(By)))
                    {
                        throw new ArgumentException("Custom finder type must be a descendent of the By class");
                    }

                    var ctor = attribute.CustomFinderType.GetConstructor(new[] {typeof(string)});
                    if (ctor == null)
                    {
                        throw new ArgumentException(
                            "Custom finder type must expose a public constructor with a string argument");
                    }

                    var finder = ctor.Invoke(new object[] {usingValue}) as By;
                    return finder;
            }

            throw new ArgumentException($"Did not know how to construct How from how {how}, using {usingValue}");
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