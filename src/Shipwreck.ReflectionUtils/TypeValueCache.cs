using System;
using System.Collections.Generic;

namespace Shipwreck.ReflectionUtils
{
    public abstract class TypeValueCache<T>
    {
        private readonly Dictionary<Type, T> _Values = new Dictionary<Type, T>();

        public T this[Type type]
            => GetValue(type);

        protected abstract T GetValueCore(Type type);

        public T GetValue(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            lock (_Values)
            {
                if (!_Values.TryGetValue(type, out var value))
                {
                    _Values[type] = value = GetValueCore(type);
                }
                return value;
            }
        }
    }
}
