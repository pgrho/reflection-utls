namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct AndAlsoPredicateMatcher<TLeft, TRight> : IPredicateMatcher
    where TLeft : IPredicateMatcher
    where TRight : IPredicateMatcher
{
    public AndAlsoPredicateMatcher(TLeft left, TRight right)
    {
        Left = left;
        Right = right;
    }

    public TLeft Left { get; }
    public TRight Right { get; }

    public readonly bool IsAlwaysMet(ParameterExpression parameter, Expression expression)
        => Left.IsAlwaysMet(parameter, expression)
        && Right.IsAlwaysMet(parameter, expression);
}
