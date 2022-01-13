namespace Shipwreck.ReflectionUtils;

public sealed class EnumMemberDisplayNames<TEnum> : EnumMemberStrings<TEnum>
    where TEnum : struct, Enum
{
    public EnumMemberDisplayNames(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
        : base(enumComparer, valueComparer)
    {
    }

    public static EnumMemberDisplayNames<TEnum> Default { get; } = new EnumMemberDisplayNames<TEnum>();

    protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
        => field?.GetCustomAttribute<DisplayAttribute>()?.GetName()
            ?? field.Name;
}
