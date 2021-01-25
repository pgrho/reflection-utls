using System;
using System.ComponentModel;
using System.Globalization;

namespace Shipwreck.ReflectionUtils
{
    public class DisplayEnumConverter<T> : EnumConverter
         where T : struct, Enum
    {
        public DisplayEnumConverter()
            : base(typeof(T))
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is T v)
            {
                return EnumMemberDisplayNames<T>.Default.GetValue(v, culture);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                if (EnumMemberDisplayNames<T>.Default.TryParseValue(s, culture, out var r)
                    || EnumMemberShortNames<T>.Default.TryParseValue(s, culture, out r))
                {
                    return r;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}

