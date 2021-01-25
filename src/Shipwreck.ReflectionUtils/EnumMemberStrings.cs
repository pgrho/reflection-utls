using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Shipwreck.ReflectionUtils
{
    public abstract class EnumMemberStrings<TEnum> : EnumMemberValues<TEnum, string>
        where TEnum : struct, Enum
    {
        public EnumMemberStrings(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
            : base(enumComparer, valueComparer ?? StringComparer.InvariantCultureIgnoreCase)
        {
            Bits = typeof(TEnum).GetCustomAttribute<FlagsAttribute>() != null ? (byte)(Marshal.SizeOf(typeof(TEnum).GetEnumUnderlyingType()) * 8)
                : typeof(TEnum).IsValueType ? 0
                : 64;
        }

        protected byte Bits { get; }

        protected virtual string Separator => ",";

        protected override bool TryGetUndefinedValue(Type type, TEnum enumValue, CultureInfo culture, Dictionary<TEnum, string> knownValues, out string value)
        {
            if (Bits > 0)
            {
                var uv = ((IConvertible)enumValue).ToUInt64(culture);

                if (uv != 0)
                {
                    value = null;
                    for (var b = 0; b < Bits; b++)
                    {
                        var f = 1ul << b;

                        if ((uv & f) != 0)
                        {
                            var ev = (TEnum)Enum.ToObject(typeof(TEnum), f);

                            if (knownValues.TryGetValue(ev, out var s))
                            {
                                value = value != null ? value + Separator + s : s;

                                uv &= ~f;
                                if (uv == 0)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return base.TryGetUndefinedValue(type, enumValue, culture, knownValues, out value);
        }

        private Regex _Splitter;

        protected override bool TryParseValueCore(string value, CultureInfo culture, Dictionary<TEnum, string> knownValues, out TEnum enumValue)
        {
            var r = base.TryParseValueCore(value, culture, knownValues, out enumValue);
            if (!r)
            {
                if (Enum.TryParse<TEnum>(value, true, out enumValue))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(value))
                {
                    ulong av = 0;

                    foreach (var v in Split(value))
                    {
                        var cs = v.Trim();
                        if (!string.IsNullOrEmpty(cs))
                        {
                            if (TryParseValueCore(cs, culture, knownValues, out var cv))
                            {
                                av |= ((IConvertible)cv).ToUInt64(culture);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    enumValue = (TEnum)Enum.ToObject(typeof(TEnum), av);
                    return true;
                }
            }
            return r;
        }

        protected virtual string[] Split(string value)
        {
            _Splitter ??= new Regex(Regex.Escape(Separator.Trim()), RegexOptions.IgnoreCase);

            return _Splitter.Split(value);
        }
    }
}
