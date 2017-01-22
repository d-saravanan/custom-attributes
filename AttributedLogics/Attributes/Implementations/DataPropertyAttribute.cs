using AttributedLogics.Attributes.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AttributedLogics.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataPropertyAttribute : Attribute
    {
        private readonly IDataSerializer _serializer = null;
        public DataPropertyAttribute()
        {
        }

        private readonly int _order;
        public int Order { get { return _order; } }

        public DataPropertyAttribute(int Order)
        {
            _order = Order;
        }

        public DataPropertyAttribute(IDataSerializer serializer)
        {
            _serializer = serializer;
        }

        public DataPropertyAttribute(int Order, IDataSerializer serializer)
        {
            _order = Order;
            _serializer = serializer;
        }

        public virtual void BindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
        }

        public virtual void UnBindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
        }

        public virtual string Serialize<T>(T data)
        {
            return _serializer.SerializeAsString(data);
        }

        public virtual T Deserialize<T>(string input)
        {
            return _serializer.DeserializeFromString<T>(input);
        }
    }
}