using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumDescriptionCache<TEnum> : EnumFieldStringValueCache<TEnum>
        where TEnum : struct, Enum
    {
        public EnumDescriptionCache(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumDescriptionCache<TEnum> Default { get; } = new EnumDescriptionCache<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetDescription();
    }
}
