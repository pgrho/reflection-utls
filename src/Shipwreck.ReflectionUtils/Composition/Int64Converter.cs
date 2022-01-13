namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct Int64Converter : IValueConverter<long>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Compare(long x, long y)
        => x.CompareTo(y);

    public bool TryConvert(object value, out long result)
    {
        switch (value)
        {
            case byte v:
                result = v;
                return true;

            case short v:
                result = v;
                return true;

            case int v:
                result = v;
                return true;

            case long v:
                result = v;
                return true;

            case sbyte v:
                result = v;
                return true;

            case ushort v:
                result = v;
                return true;

            case uint v:
                result = v;
                return true;

            case ulong v:
                result = unchecked((long)v);
                return true;

            case float v:
                result = (long)v;
                return true;

            case double v:
                result = (long)v;
                return true;

            case decimal v:
                result = (long)v;
                return true;

            case IConvertible v:
                try
                {
                    result = v.ToInt64(null);
                    return true;
                }
                catch { }
                break;
        }
        result = 0;
        return false;
    }
}
