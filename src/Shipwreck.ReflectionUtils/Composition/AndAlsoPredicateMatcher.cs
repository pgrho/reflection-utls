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
    {
        if (expression is BinaryExpression be)
        {
            if (be.NodeType == ExpressionType.And || be.NodeType == ExpressionType.AndAlso)
            {
                if (Left.IsAlwaysMet(parameter, be.Left) && Right.IsAlwaysMet(parameter, be.Right))
                {
                    return true;
                }
                if (Right.IsAlwaysMet(parameter, be.Left) && Left.IsAlwaysMet(parameter, be.Right))
                {
                    return true;
                }
            }
        }
        return Left.IsAlwaysMet(parameter, expression) && Right.IsAlwaysMet(parameter, expression);
    }
}
