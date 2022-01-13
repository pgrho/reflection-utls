namespace Shipwreck.ReflectionUtils.Composition;

public interface IPredicateMatcher
{
    bool IsAlwaysMet(ParameterExpression parameter, Expression expression);
}
