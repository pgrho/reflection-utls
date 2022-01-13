using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Shipwreck.ReflectionUtils;

public class DisplayTypeAttributeTest
{
    private class Type1
    { }

    [DisplayType(Name = "2")]
    private class Type2
    { }

    [DisplayType(Name = "Test", ResourceType = typeof(Resource))]
    private class Type3
    { }

    [Theory]
    [InlineData(typeof(Type1), null)]
    [InlineData(typeof(Type2), "2")]
    public void Test(Type t, string expected)
        => Assert.Equal(t.GetCustomAttribute<DisplayTypeAttribute>()?.GetName(), expected);

    [Theory]
    [InlineData(typeof(Type3), "en", "Test")]
    [InlineData(typeof(Type3), "ja", "テスト")]
    public void LocaleTest(Type t, string lcid, string expected)
    {
        var c = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lcid);
        Assert.Equal(t.GetCustomAttribute<DisplayTypeAttribute>()?.GetName(), expected);
        Thread.CurrentThread.CurrentCulture = c;
    }
}
