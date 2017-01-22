using AttributedLogics.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributedLogics.Extensions
{
    public static class TypeExtensions
    {
        private static Func<Type, Dictionary<string, PropertyInfo>> _getPropertiesForType = (objType) =>
        {
            if (TypePropertyStore._.ContainsKey(objType)) return TypePropertyStore._[objType];
            var props = objType.GetProperties().Where(p => p.CustomAttributes != null).ToDictionary(t => t.Name, t => t);

            if (TypePropertyStore._.TryAdd(objType, props)) return props;

            return new Dictionary<string, PropertyInfo>();
        };

        public static T Bind<T>(this T tObj)
        {
            var props = _getPropertiesForType(typeof(T));

            var attributedProperties = extractPropertiesWithOrderedAttributes(props.Values, true);

            if (attributedProperties != null)
                foreach (var prop in attributedProperties)
                {
                    foreach (var attr in prop.Attribute)
                    {
                        attr.BindAttributes(prop.Property, attr, props.Values, tObj);
                    }
                }
            return tObj;
        }

        public static T Unbind<T>(this T tObj)
        {
            var props = _getPropertiesForType(typeof(T));

            var attributedProperties = extractPropertiesWithOrderedAttributes(props.Values, false);
            if (attributedProperties != null)
                foreach (var prop in attributedProperties)
                {
                    foreach (var attr in prop.Attribute)
                    {
                        attr.UnBindAttributes(prop.Property, attr, props.Values, tObj);
                    }
                }
            return tObj;
        }

        private static Func<IEnumerable<PropertyInfo>, bool, IEnumerable<dynamic>> extractPropertiesWithOrderedAttributes = (props, asc) =>
        {
            return from prop in props
                   let dpAttr = prop.GetCustomAttributes<DataPropertyAttribute>()
                   where dpAttr != null && dpAttr.Count() > 0
                   select new { Property = prop, Attribute = asc ? dpAttr.OrderBy(d => d.Order) : dpAttr.OrderByDescending(d => d.Order) };
        };

        private static Func<IEnumerable<PropertyInfo>, IEnumerable<dynamic>> extractPropertiesWithAttributes = (props) =>
        {
            return from prop in props
                   let dpAttr = prop.GetCustomAttributes<DataPropertyAttribute>()
                   where dpAttr != null && dpAttr.Count() > 0
                   select new { Property = prop, Attribute = dpAttr };
        };
    }
}
