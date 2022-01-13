namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct MemberExpressionMatcher : IPredicateMatcher
{
    public MemberExpressionMatcher(string expectedName, Type expectedType = null)
    {
        ExpectedName = expectedName;
        ExpectedType = expectedType;
    }

    public string ExpectedName { get; }
    public Type ExpectedType { get; }

    public readonly bool IsAlwaysMet(ParameterExpression parameter, Expression expression)
    {
        if (expression is MemberExpression me)
        {
            return me.Expression == parameter
                && me.Member.Name == ExpectedName
                && ExpectedType?.IsAssignableFrom(me.Type) != false;
        }
        if (expression is UnaryExpression ue
            && (ue.NodeType == ExpressionType.Convert || ue.NodeType == ExpressionType.ConvertChecked))
        {
            return IsAlwaysMet(parameter, ue.Operand);
        }
        return false;
    }
}

public readonly struct MemberExpressionMatcher<TExpressionMatcher> : IPredicateMatcher
    where TExpressionMatcher : IPredicateMatcher
{
    public MemberExpressionMatcher(TExpressionMatcher expressionMatcher, string expectedName, Type expectedType = null)
    {
        ExpressionMatcher = expressionMatcher;
        ExpectedName = expectedName;
        ExpectedType = expectedType;
    }

    public TExpressionMatcher ExpressionMatcher { get; }

    public string ExpectedName { get; }
    public Type ExpectedType { get; }

    public readonly bool IsAlwaysMet(ParameterExpression parameter, Expression expression)
    {
        if (expression is MemberExpression me)
        {
            return ExpressionMatcher.IsAlwaysMet(parameter, me.Expression)
                && me.Member.Name == ExpectedName
                && ExpectedType?.IsAssignableFrom(me.Type) != false;
        }
        if (expression is UnaryExpression ue
            && (ue.NodeType == ExpressionType.Convert || ue.NodeType == ExpressionType.ConvertChecked))
        {
            return IsAlwaysMet(parameter, ue.Operand);
        }
        return false;
    }
}
