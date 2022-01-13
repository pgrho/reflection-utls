namespace Shipwreck.ReflectionUtils;

/// <summary>
/// 型の表示名称を指定します。
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
public class DisplayTypeAttribute : DisplayAttributeBase
{
    public DisplayTypeAttribute()
    {
    }

    /// <summary>
    /// <see cref="DisplayTypeAttribute" />クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="name">指定対象の型の表示名称またはリソースのキー。</param>
    /// <param name="resourceType">名称を検索するリソースの型。</param>
    public DisplayTypeAttribute(string name, Type resourceType = null)
    {
        Name = name;
        ResourceType = resourceType;
    }
}
