using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Shipwreck.ReflectionUtils
{
    public abstract class LocalizedValues<T>
    {
        private readonly Dictionary<CultureInfo, T> _Values = new Dictionary<CultureInfo, T>();

        public T this[CultureInfo culture]
            => GetValue(culture);

        protected abstract T GetValueCore(CultureInfo culture);

        public T GetValue()
            => GetValue(Thread.CurrentThread.CurrentCulture);

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

        public void Clear()
        {
            lock (_Values)
            {
                _Values.Clear();
            }
        }
    }
}

