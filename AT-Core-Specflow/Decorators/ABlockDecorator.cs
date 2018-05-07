using System;
using System.Collections.Generic;
using System.Reflection;
using AT_Core_Specflow.CustomElements.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
namespace AT_Core_Specflow.Decorators
{
    public class ABlockDecorator : BaseDecorator, IPageObjectMemberDecorator
    {
        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            if (!FieldNeedDecorated(member)) return null;
            var cache = ShouldCacheLookup(member);
            var targetType = GetElementType(member);
            var elementTitle = GetElementTitle(member, targetType);

            IList<By> bys = CreateLocatorList(member, targetType);
            if (bys.Count <= 0) return null;
            if (CheckElementType(targetType))
            {
                var element = Activator.CreateInstance(targetType, locator, bys, cache, elementTitle);
                var blockName =
                    ((BlockTitleAttribute)member.DeclaringType.GetCustomAttribute(typeof(BlockTitleAttribute)))
                    .Title;
                AddElementToBlock(blockName, element, elementTitle);
                SetTimeOutSearch(member, targetType, element);
                return element;
            }
            throw new NotImplementedException(
                $"Класс элемента \"{member.DeclaringType}.{targetType.Name}\" не является классом, который может быть декорирован.\nДекорирование возможно для класса \"ImlList<>\", а так же для наследников классов \"ImlBlockElement\" и \"ImlElement\"");
        }
    }
}