namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct DateTimeOffsetConverter : IValueConverter<DateTimeOffset>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Compare(DateTimeOffset x, DateTimeOffset y)
        => x.CompareTo(y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryConvert(object value, out DateTimeOffset result)
    {
        switch (value)
        {
            case DateTime v:
                result = v;
                return true;

            case DateTimeOffset v:
                result = v;
                return true;
        }
        result = default;
        return false;
    }
}
