using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumMemberShortNames<TEnum> : EnumMemberStrings<TEnum>
        where TEnum : struct, Enum
    {
        public EnumMemberShortNames(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumMemberShortNames<TEnum> Default { get; } = new EnumMemberShortNames<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetShortName()
            ?? field.Name;
    }
}
