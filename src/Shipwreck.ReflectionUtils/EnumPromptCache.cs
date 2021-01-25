using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Shipwreck.ReflectionUtils
{
    public sealed class EnumPromptCache<TEnum> : EnumFieldStringValueCache<TEnum>
        where TEnum : struct, Enum
    {
        public EnumPromptCache(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer)
        {
        }

        public static EnumPromptCache<TEnum> Default { get; } = new EnumPromptCache<TEnum>();

        protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
            => field?.GetCustomAttribute<DisplayAttribute>()?.GetPrompt();
    }
}
