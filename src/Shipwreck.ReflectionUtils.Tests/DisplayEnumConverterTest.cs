using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Xunit;

namespace Shipwreck.ReflectionUtils;

public class DisplayEnumConverterTest
{
    [DataContract]
    public enum TestEnum
    {
        [EnumMember]
        [Display(Name = "ホゲ")]
        Hoge = 2,
    }

    [Fact]
    public void Test()
    {
        try
        {
            new DisplayEnumConverter<TestEnum>().ConvertFrom("あああ");
            Assert.Fail();
        }
        catch { }
    }
}
