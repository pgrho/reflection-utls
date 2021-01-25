using System;
using System.Globalization;
using System.Threading;

namespace Shipwreck.ReflectionUtils
{
    public abstract class LocalizedTypeValues<T> : TypeValues<LocalizedValues<T>>
    {
        private sealed class LocalizedValues : LocalizedValues<T>
        {
            private readonly LocalizedTypeValues<T> _Parent;
            private readonly Type _Type;

            public LocalizedValues(LocalizedTypeValues<T> parent, Type type)
            {
                _Parent = parent;
                _Type = type;
            }

            protected override T GetValueCore(CultureInfo culture)
                => _Parent.GetValueCore(_Type, culture);
        }

        public new T this[Type type]
            => GetValue(type, Thread.CurrentThread.CurrentCulture);

        public T this[Type type, CultureInfo culture]
            => GetValue(type, culture);

        protected sealed override LocalizedValues<T> GetValueCore(Type type)
            => new LocalizedValues(this, type ?? throw new ArgumentNullException(nameof(type)));

        public new T GetValue(Type type)
            => GetValue(type, Thread.CurrentThread.CurrentCulture);

        public T GetValue(Type type, CultureInfo culture)
            => base.GetValue(type).GetValue(culture);

        protected abstract T GetValueCore(Type type, CultureInfo culture);
    }
}
