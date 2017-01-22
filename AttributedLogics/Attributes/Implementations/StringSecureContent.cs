using AttributedLogics.Attributes.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AttributedLogics.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringSecureContent : DataPropertyAttribute
    {
        public StringSecureContent(int Order)
            : base(Order)
        {
        }

        private readonly IDataProtector _dataProtector;
        public StringSecureContent(int Order, IDataProtector dataProtector)
            : base(Order)
        {
            _dataProtector = dataProtector;
        }

        public override void BindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
            source.SetValue(tObj, _dataProtector.Protect<string, string>((string)source.GetValue(tObj)));
        }

        public override void UnBindAttributes<T>(PropertyInfo source, DataPropertyAttribute attribute, IEnumerable<PropertyInfo> properties, T tObj)
        {
            source.SetValue(tObj, _dataProtector.UnProtect<string, string>((string)source.GetValue(tObj)));
        }
    }
}