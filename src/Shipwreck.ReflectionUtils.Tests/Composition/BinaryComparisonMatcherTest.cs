using System;
using System.Linq.Expressions;
using Xunit;

namespace Shipwreck.ReflectionUtils.Composition;

public class BinaryComparisonMatcherTest
{
    class Arg
    {
        public long Long { get; set; }
        public bool B { get; set; }
    }

    [Fact]
    public void LongTrueTest()
    {
        var matcher = PredicateMatcherHelper.MemberMatcher("Long").Equal(1);
        {
            Expression<Func<Arg, bool>> p = e => e.Long == 1;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.True(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.Long != 1;
            Assert.False(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.False(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
    }

    [Fact]
    public void BooleanTrueTest()
    {
        var matcher = PredicateMatcherHelper.MemberMatcher("B").Equal(true);
        {
            Expression<Func<Arg, bool>> p = e => e.B == true;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.True(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.B != true;
            Assert.False(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.False(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.B;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.True(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
        {
            Expression<Func<Arg, bool>> p = e => !e.B;
            Assert.False(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.False(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
        {
            Expression<Func<Arg, bool>> p = e => !!e.B;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
            Assert.True(ExpressionHelper.IsAlwaysMet(p.Parameters[0], p.Body, (a, e) => matcher.IsAlwaysMet(a, e)));
        }
    }
}
