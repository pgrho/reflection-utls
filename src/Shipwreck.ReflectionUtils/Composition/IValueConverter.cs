namespace Shipwreck.ReflectionUtils.Composition;

public interface IValueConverter<T> : IComparer<T>
{
    bool TryConvert(object value, out T result);
}
