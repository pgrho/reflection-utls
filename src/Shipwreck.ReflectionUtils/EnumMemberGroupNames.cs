namespace Shipwreck.ReflectionUtils;

public sealed class EnumMemberGroupNames<TEnum> : EnumMemberStrings<TEnum>
    where TEnum : struct, Enum
{
    public EnumMemberGroupNames(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
        : base(enumComparer, valueComparer)
    {
    }

    public static EnumMemberGroupNames<TEnum> Default { get; } = new EnumMemberGroupNames<TEnum>();

    protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
        => field?.GetCustomAttribute<DisplayAttribute>()?.GetGroupName();
}
