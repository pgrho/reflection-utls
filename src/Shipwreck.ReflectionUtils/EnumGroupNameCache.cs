using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumGroupNameCache<TEnum> : EnumFieldStringValueCache<TEnum>
        where TEnum : struct, Enum
    {
        public EnumGroupNameCache(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumGroupNameCache<TEnum> Default { get; } = new EnumGroupNameCache<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetGroupName();
    }
}
