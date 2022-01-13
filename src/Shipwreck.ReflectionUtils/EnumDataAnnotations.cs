namespace Shipwreck.ReflectionUtils;

public static class EnumDataAnnotations
{
    private static readonly Dictionary<Type, IEnumDataAnnotations> _Instances = new();

    public static IEnumDataAnnotations Get(Type type)
    {
        lock (_Instances)
        {
            if (!_Instances.TryGetValue(type, out var r))
            {
                _Instances[type] = r = (IEnumDataAnnotations)Activator.CreateInstance(typeof(EnumDataAnnotations<>).MakeGenericType(type));
            }
            return r;
        }
    }
}

public sealed class EnumDataAnnotations<T> : IEnumDataAnnotations
    where T : struct, Enum
{
    public string GetDisplayName(object value)
        => EnumMemberDisplayNames<T>.Default.GetValue((T)value);

    public string GetShortName(object value)
        => EnumMemberShortNames<T>.Default.GetValue((T)value);

    public string GetGroupName(object value)
        => EnumMemberGroupNames<T>.Default.GetValue((T)value);

    public string GetDescription(object value)
        => EnumMemberDescriptions<T>.Default.GetValue((T)value);

    public string GetPrompt(object value)
        => EnumMemberPrompts<T>.Default.GetValue((T)value);
}
