namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct BooleanConverter : IValueConverter<bool>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Compare(bool x, bool y)
        => x.CompareTo(y);

    public bool TryConvert(object value, out bool result)
    {
        switch (value)
        {
            case bool v:
                result = v;
                return true;

            case byte v:
                result = v != 0;
                return true;

            case short v:
                result = v != 0;
                return true;

            case int v:
                result = v != 0;
                return true;

            case long v:
                result = v != 0;
                return true;

            case sbyte v:
                result = v != 0;
                return true;

            case ushort v:
                result = v != 0;
                return true;

            case uint v:
                result = v != 0;
                return true;

            case ulong v:
                result = v != 0;
                return true;

            case float v:
                result = v != 0; ;
                return true;

            case double v:
                result = v != 0;
                return true;

            case decimal v:
                result = v != 0; ;
                return true;

            case IConvertible v:
                try
                {
                    result = v.ToBoolean(null);
                    return true;
                }
                catch { }
                break;
        }
        result = false;
        return false;
    }
}
