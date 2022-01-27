using Shipwreck.ReflectionUtils.Composition;

namespace Shipwreck.ReflectionUtils;

public static class PredicateMatcherHelper
{
    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AndAlsoPredicateMatcher<TLeft, TRight> AndAlso<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IPredicateMatcher
        where TRight : IPredicateMatcher
        => new(left, right);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static OrElsePredicateMatcher<TLeft, TRight> OrElse<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IPredicateMatcher
        where TRight : IPredicateMatcher
        => new(left, right);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MemberExpressionMatcher MemberMatcher(string expectedName, Type expectedType = null)
        => new(expectedName, expectedType);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MemberExpressionMatcher<TExpressionMatcher> MemberMatcher<TExpressionMatcher>(this TExpressionMatcher expressionMatcher, string expectedName, Type expectedType = null)
        where TExpressionMatcher : IPredicateMatcher
        => new(expressionMatcher, expectedName, expectedType);

    #region Boolean

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> Equal<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.Equal, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> NotEqual<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.NotEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> GreaterThan<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> GreaterThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThanOrEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> LessThan<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<bool, BooleanConverter, TValueMatcher> LessThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, bool testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThanOrEqual, testingOperand);

    #endregion Boolean

    #region Int64

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> Equal<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.Equal, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> NotEqual<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.NotEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> GreaterThan<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> GreaterThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThanOrEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> LessThan<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<long, Int64Converter, TValueMatcher> LessThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, long testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThanOrEqual, testingOperand);

    #endregion Int64

    #region DateTime

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> Equal<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.Equal, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> NotEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.NotEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> GreaterThan<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> GreaterThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThanOrEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> LessThan<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTime, DateTimeConverter, TValueMatcher> LessThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTime testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThanOrEqual, testingOperand);

    #endregion DateTime

    #region DateTimeOffset

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> Equal<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.Equal, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> NotEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.NotEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> GreaterThan<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> GreaterThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.GreaterThanOrEqual, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> LessThan<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThan, testingOperand);

    [TargetedPatchingOptOut("")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BinaryComparisonMatcher<DateTimeOffset, DateTimeOffsetConverter, TValueMatcher> LessThanOrEqual<TValueMatcher>(this TValueMatcher valueMatcher, DateTimeOffset testingOperand)
        where TValueMatcher : IPredicateMatcher
        => new(default, valueMatcher, ExpressionType.LessThanOrEqual, testingOperand);

    #endregion DateTimeOffset
}
