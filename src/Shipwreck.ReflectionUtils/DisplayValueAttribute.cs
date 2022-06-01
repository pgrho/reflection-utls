namespace Shipwreck.ReflectionUtils;

/// <summary>
/// プロパティ値の名称を指定します。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public sealed class DisplayValueAttribute : DisplayAttributeBase
{
    public DisplayValueAttribute(object value)
    {
        Value = value;
    }

    /// <summary>
    /// <see cref="DisplayValueAttribute" />クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="value">指定対象のプロパティ値。</param>
    /// <param name="name">指定対象のプロパティの名称またはリソースのキー。</param>
    /// <param name="resourceType">名称を検索するリソースの型。</param>
    public DisplayValueAttribute(object value, string name, Type resourceType = null)
    {
        Value = value;
        Name = name;
        ResourceType = resourceType;
    }

    /// <summary>
    /// 指定対象のプロパティ値を取得します。
    /// </summary>
    public object Value { get; }
}
