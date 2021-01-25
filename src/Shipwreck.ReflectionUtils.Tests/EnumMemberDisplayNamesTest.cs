using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Shipwreck.ReflectionUtils
{
    public class EnumMemberDisplayNamesTest
    {
        public enum EnumType
        {
            [Display(Name = "1")]
            One,

            [Display(Name = "two")]
            Two,

            Three
        }

        [Flags]
        public enum FlagsType
        {
            [Display(Name = "1st")]
            First = 1,

            [Display(Name = "second")]
            Second = 2,

            Third = 4
        }

        [Theory]
        [InlineData(EnumType.One, "1")]
        [InlineData(EnumType.Two, "two")]
        [InlineData(EnumType.Three, "Three")]
        public void GetValueTest(EnumType enumValue, string expected)
        {
            var cache = new EnumMemberDisplayNames<EnumType>();
            Assert.Equal(expected, cache.GetValue(enumValue));
            Assert.Equal(enumValue, cache.ParseValue(expected));
        }

        [Theory]
        [InlineData(FlagsType.First, "1st")]
        [InlineData(FlagsType.Second, "second")]
        [InlineData(FlagsType.Third, "Third")]
        [InlineData(FlagsType.First | FlagsType.Second, "1st,second")]
        public void GetValueTest_Flags(FlagsType enumValue, string expected)
        {
            var cache = new EnumMemberDisplayNames<FlagsType>();
            Assert.Equal(expected, cache.GetValue(enumValue));
            Assert.Equal(enumValue, cache.ParseValue(expected));
        }
    }
}
