namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct OrElsePredicateMatcher<TLeft, TRight> : IPredicateMatcher
    where TLeft : IPredicateMatcher
    where TRight : IPredicateMatcher
{
    public OrElsePredicateMatcher(TLeft left, TRight right)
    {
        Left = left;
        Right = right;
    }

    public TLeft Left { get; }
    public TRight Right { get; }

    public readonly bool IsAlwaysMet(ParameterExpression parameter, Expression expression)
        => Left.IsAlwaysMet(parameter, expression)
        || Right.IsAlwaysMet(parameter, expression);
}
