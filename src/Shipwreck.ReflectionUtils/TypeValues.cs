namespace Shipwreck.ReflectionUtils;

public abstract class TypeValues<T>
{
    private readonly Dictionary<Type, T> _Values = new();

    public T this[Type type]
        => GetValue(type);

    protected abstract T GetValueCore(Type type);

    public T GetValue(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        lock (_Values)
        {
            if (!_Values.TryGetValue(type, out var value))
            {
                _Values[type] = value = GetValueCore(type);
            }
            return value;
        }
    }

    public void Clear()
    {
        lock (_Values)
        {
            _Values.Clear();
        }
    }
}
