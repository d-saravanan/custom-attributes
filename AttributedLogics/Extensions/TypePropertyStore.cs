using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace AttributedLogics.Extensions
{
    internal static class TypePropertyStore
    {
        internal static ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _ = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();
    }
}
