using System;
using System.Collections.Generic;
using System.Globalization;

namespace Shipwreck.ReflectionUtils
{
    public abstract class CultureValueCache<T>
    {
        private readonly Dictionary<CultureInfo, T> _Values = new Dictionary<CultureInfo, T>();

        public T this[CultureInfo culture]
            => GetValue(culture);

        protected abstract T GetValueCore(CultureInfo culture);

        public T GetValue(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }
            lock (_Values)
            {
                if (!_Values.TryGetValue(culture, out var value))
                {
                    _Values[culture] = value = GetValueCore(culture);
                }
                return value;
            }
        }
    }
}
