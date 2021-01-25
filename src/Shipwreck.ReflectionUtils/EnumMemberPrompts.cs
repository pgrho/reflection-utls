using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumMemberPrompts<TEnum> : EnumMemberStrings<TEnum>
        where TEnum : struct, Enum
    {
        public EnumMemberPrompts(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumMemberPrompts<TEnum> Default { get; } = new EnumMemberPrompts<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetPrompt();
    }
}
