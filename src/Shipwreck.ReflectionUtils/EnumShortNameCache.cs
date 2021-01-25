using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumShortNameCache<TEnum> : EnumFieldStringValueCache<TEnum>
        where TEnum : struct, Enum
    {
        public EnumShortNameCache(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumShortNameCache<TEnum> Default { get; } = new EnumShortNameCache<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetShortName()
            ?? field.Name;
    }
}
