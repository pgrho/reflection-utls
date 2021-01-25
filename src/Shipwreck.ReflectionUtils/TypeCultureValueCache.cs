using System;
using System.Globalization;
using System.Threading;

namespace Shipwreck.ReflectionUtils
{
    public abstract class TypeCultureValueCache<T> : TypeValueCache<CultureValueCache<T>>
    {
        private sealed class CultureValueCache : CultureValueCache<T>
        {
            private readonly TypeCultureValueCache<T> _Parent;
            private readonly Type _Type;

            public CultureValueCache(TypeCultureValueCache<T> parent, Type type)
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

        protected sealed override CultureValueCache<T> GetValueCore(Type type)
            => new CultureValueCache(this, type ?? throw new ArgumentNullException(nameof(type)));

        public new T GetValue(Type type)
            => GetValue(type, Thread.CurrentThread.CurrentCulture);

        public T GetValue(Type type, CultureInfo culture)
            => base.GetValue(type).GetValue(culture);

        protected abstract T GetValueCore(Type type, CultureInfo culture);
    }
}
