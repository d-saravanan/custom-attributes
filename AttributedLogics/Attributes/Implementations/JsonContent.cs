using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributedLogics.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonContent : DataPropertyAttribute
    {
        public string SourceColumnNames { get { return _srcColumnNames; } }
        private string _srcColumnNames;
        public JsonContent(string colName)
        {
            _srcColumnNames = colName;
        }

        public JsonContent(string colName, int Order)
            : base(Order)
        {
            _srcColumnNames = colName;
        }

        public override void BindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
            var thisAttribute = attribute as JsonContent;
            if (thisAttribute == null) return;

            var columns = thisAttribute.SourceColumnNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (columns.Length < 1) return;

            var dataToSerialize = CollectPropertyData(properties, columns, tObj);

            InjectSerializedValue(source, Serialize(dataToSerialize), tObj);
        }

        public override void UnBindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
            var thisAttribute = attribute as JsonContent;
            if (thisAttribute == null) return;

            var columns = thisAttribute.SourceColumnNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (columns.Length < 1) return;

            var data = Deserialize<Dictionary<string, object>>((string)source.GetValue(tObj));
            SplitAndInject(properties, data, tObj);
        }

        private static Func<IEnumerable<PropertyInfo>, string[], object, Dictionary<string, object>> CollectPropertyData =
            (props, columnNames, tObj) =>
            {
                var jsonContent = props.Where(p => columnNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase)).Select(x => new
                {
                    Key = x.Name,
                    Value = x.GetValue(tObj)
                }).ToDictionary(t => t.Key, t => t.Value);
                return jsonContent;
            };

        private static Action<PropertyInfo, object, object> InjectSerializedValue = (prop, value, tObj) =>
            {
                if (prop.CanWrite)
                    prop.SetValue(tObj, value);
            };

        private static Action<IEnumerable<PropertyInfo>, Dictionary<string, object>, object> SplitAndInject = (props, deserializedData, tObj) =>
        {
            foreach (var data in deserializedData)
            {
                var prop = props.SingleOrDefault(s => s.Name.Equals(data.Key, StringComparison.OrdinalIgnoreCase));
                if (prop == null || !prop.CanWrite) continue;
                prop.SetValue(tObj, data.Value);
            }
        };

    }
}