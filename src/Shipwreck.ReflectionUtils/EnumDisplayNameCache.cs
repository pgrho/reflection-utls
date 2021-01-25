using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumDisplayNameCache<TEnum> : EnumFieldStringValueCache<TEnum>
        where TEnum : struct, Enum
    {
        public EnumDisplayNameCache(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumDisplayNameCache<TEnum> Default { get; } = new EnumDisplayNameCache<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetName()
                ?? field.Name;
    }
}
