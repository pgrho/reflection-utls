namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct DateTimeConverter : IValueConverter<DateTime>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Compare(DateTime x, DateTime y)
        => x.CompareTo(y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryConvert(object value, out DateTime result)
    {
        switch (value)
        {
            case DateTime v:
                result = v;
                return true;

            case DateTimeOffset v:
                result = v.UtcDateTime;
                return true;
        }
        result = default;
        return false;
    }
}
