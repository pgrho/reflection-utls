namespace Shipwreck.ReflectionUtils;

public sealed class EnumMemberDescriptions<TEnum> : EnumMemberStrings<TEnum>
    where TEnum : struct, Enum
{
    public EnumMemberDescriptions(IEqualityComparer<TEnum> enumComparer = null, IEqualityComparer<string> valueComparer = null)
        : base(enumComparer, valueComparer)
    {
    }

    public static EnumMemberDescriptions<TEnum> Default { get; } = new EnumMemberDescriptions<TEnum>();

    protected override string GetValueCore(Type type, FieldInfo field, TEnum value, CultureInfo culture)
        => field?.GetCustomAttribute<DisplayAttribute>()?.GetDescription();
}
