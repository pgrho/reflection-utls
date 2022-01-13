namespace Shipwreck.ReflectionUtils;

public interface IEnumDataAnnotations
{
    string GetDisplayName(object value);

    string GetShortName(object value);

    string GetGroupName(object value);

    string GetDescription(object value);

    string GetPrompt(object value);
}
