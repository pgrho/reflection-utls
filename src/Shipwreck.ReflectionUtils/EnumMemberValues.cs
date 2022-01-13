using System.Threading;

namespace Shipwreck.ReflectionUtils;

public abstract class EnumMemberValues<TEnum, TValue> : LocalizedTypeValues<Dictionary<TEnum, TValue>>
    where TEnum : Enum
{
    protected override Dictionary<TEnum, TValue> GetValueCore(Type type, CultureInfo culture)
    {
        var dic = new Dictionary<TEnum, TValue>(EnumComparer);
        var th = Thread.CurrentThread;
        var c = th.CurrentCulture;
        var ui = th.CurrentUICulture;
        try
        {
            th.CurrentCulture = th.CurrentUICulture = culture;

            foreach (var f in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (f.FieldType == typeof(TEnum))
                {
                    var v = (TEnum)f.GetValue(null);
                    dic[v] = GetValueCore(type, f, v, culture);
                }
            }
        }
        finally
        {
            th.CurrentCulture = c;
            th.CurrentUICulture = ui;
        }
        return dic;
    }

    public EnumMemberValues(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<TValue> valueComparer = null)
    {
        EnumComparer = enumComparer ?? EqualityComparer<TEnum>.Default;
        ValueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

    public IEqualityComparer<TEnum> EnumComparer { get; }
    public IEqualityComparer<TValue> ValueComparer { get; }

    public TValue this[TEnum enumValue]
        => GetValue(enumValue, Thread.CurrentThread.CurrentCulture);

    public TValue this[TEnum enumValue, CultureInfo culture]
        => GetValue(enumValue, culture);

    protected abstract TValue GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture);

    protected virtual bool TryGetUndefinedValue(Type type, TEnum enumValue, CultureInfo culture, Dictionary<TEnum, TValue> knownValues, out TValue value)
    {
        value = default;
        return false;
    }

    public bool TryGetValue(TEnum enumValue, out TValue value)
        => TryGetValue(enumValue, Thread.CurrentThread.CurrentCulture, out value);

    public bool TryGetValue(TEnum enumValue, CultureInfo culture, out TValue value)
    {
        var d = GetValue(typeof(TEnum), culture);
        lock (((ICollection)d).SyncRoot)
        {
            if (d.TryGetValue(enumValue, out value))
            {
                return true;
            }
            else if (TryGetUndefinedValue(typeof(TEnum), enumValue, culture, d, out value))
            {
                d[enumValue] = value;
                return true;
            }
            return false;
        }
    }

    public TValue GetValue(TEnum enumValue)
        => GetValue(enumValue, Thread.CurrentThread.CurrentCulture);

    public TValue GetValue(TEnum enumValue, CultureInfo culture)
        => TryGetValue(enumValue, culture, out var r) ? r
        : throw new InvalidOperationException(string.Format("Value '{0}' of {1} is not supported in {2}", enumValue, typeof(TEnum).FullName, GetType().FullName));

    #region TryParseValue

    public bool TryParseValue(TValue value, out TEnum enumValue)
    => TryParseValue(value, Thread.CurrentThread.CurrentCulture, out enumValue);

    public bool TryParseValue(TValue value, CultureInfo culture, out TEnum enumValue)
    {
        var d = GetValue(typeof(TEnum), culture);
        lock (((ICollection)d).SyncRoot)
        {
            foreach (var kv in d)
            {
                if (ValueComparer.Equals(kv.Value, value))
                {
                    enumValue = kv.Key;
                    return true;
                }
            }
            if (TryParseValueCore(value, culture, d, out enumValue))
            {
                return true;
            }
            return false;
        }
    }

    public TEnum ParseValue(TValue value)
        => ParseValue(value, Thread.CurrentThread.CurrentCulture);

    public TEnum ParseValue(TValue value, CultureInfo culture)
        => TryParseValue(value, culture, out var r) ? r
        : throw new InvalidOperationException(string.Format("Value '{0}' of {1} is not supported in {2}", value, (value?.GetType().FullName ?? "null"), GetType().FullName));

    protected virtual bool TryParseValueCore(TValue value, CultureInfo culture, Dictionary<TEnum, TValue> knownValues, out TEnum enumValue)
    {
        enumValue = default;
        return false;
    }

    #endregion TryParseValue
}
