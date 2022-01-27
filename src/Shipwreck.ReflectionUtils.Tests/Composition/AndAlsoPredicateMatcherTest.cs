using System;
using System.Linq.Expressions;
using Xunit;

namespace Shipwreck.ReflectionUtils.Composition;

public class AndAlsoPredicateMatcherTest
{
    class Arg
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }

    [Fact]
    public void Test()
    {
        var matcher = PredicateMatcherHelper.MemberMatcher("A").Equal(1).AndAlso(PredicateMatcherHelper.MemberMatcher("B").Equal(2));

        {
            Expression<Func<Arg, bool>> p = e => e.A == 1 && e.B == 2;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.A == 0 && e.B == 2;
            Assert.False(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.A == 1 && e.B == 0;
            Assert.False(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.B == 2 && e.A == 1;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
        }
        {
            Expression<Func<Arg, bool>> p = e => e.B == 2 && e.C == 4 && e.A == 1;
            Assert.True(matcher.IsAlwaysMet(p.Parameters[0], p.Body));
        }
    }
}
