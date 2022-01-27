namespace Shipwreck.ReflectionUtils.Composition;

public readonly struct BinaryComparisonMatcher<TValue, TConverter, TValueMatcher> : IPredicateMatcher
    where TConverter : IValueConverter<TValue>
    where TValueMatcher : IPredicateMatcher
{
    public BinaryComparisonMatcher(
        TConverter converter,
        TValueMatcher valueMatcher,
        ExpressionType testingComparison,
        TValue testingOperand)
    {
        Converter = converter;
        ValueMatcher = valueMatcher;
        TestingComparison = testingComparison;
        TestingOperand = testingOperand;
    }

    public TConverter Converter { get; }
    public TValueMatcher ValueMatcher { get; }
    public ExpressionType TestingComparison { get; }
    public TValue TestingOperand { get; }

    public readonly bool IsAlwaysMet(ParameterExpression parameter, Expression expression)
    {
        if (expression is BinaryExpression b)
        {
            object op;
            ExpressionType et;
            if (ValueMatcher.IsAlwaysMet(parameter, b.Left))
            {
                switch (b.NodeType)
                {
                    case ExpressionType.Equal:
                    case ExpressionType.NotEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                        et = b.NodeType;
                        break;

                    default:
                        return false;
                }
                if (!b.Right.TryGetConstant(out op))
                {
                    return false;
                }
            }
            else if (ValueMatcher.IsAlwaysMet(parameter, b.Right))
            {
                switch (b.NodeType)
                {
                    case ExpressionType.Equal:
                    case ExpressionType.NotEqual:
                        et = b.NodeType;
                        break;

                    case ExpressionType.GreaterThan:
                        et = ExpressionType.LessThan;
                        break;

                    case ExpressionType.GreaterThanOrEqual:
                        et = ExpressionType.LessThanOrEqual;
                        break;

                    case ExpressionType.LessThan:
                        et = ExpressionType.GreaterThan;
                        break;

                    case ExpressionType.LessThanOrEqual:
                        et = ExpressionType.GreaterThanOrEqual;
                        break;

                    default:
                        return false;
                }

                if (!b.Left.TryGetConstant(out op))
                {
                    return false;
                }
            }
            else if (b.NodeType == ExpressionType.And || b.NodeType == ExpressionType.AndAlso)
            {
                return IsAlwaysMet(parameter, b.Left) || IsAlwaysMet(parameter, b.Right);
            }
            else if (b.NodeType == ExpressionType.Or || b.NodeType == ExpressionType.OrElse)
            {
                return IsAlwaysMet(parameter, b.Left) && IsAlwaysMet(parameter, b.Right);
            }
            else
            {
                return false;
            }

            if (!Converter.TryConvert(op, out var v))
            {
                return false;
            }

            var c = Converter.Compare(v, TestingOperand);

            return ExpressionTypeHelper.IsAlwaysMet(TestingComparison, et, c);
        }
        else if (expression is MemberExpression me
            && me.Type == typeof(bool)
            && typeof(TValue) == typeof(bool))
        {
            if (IsAlwaysMet(parameter, Expression.Equal(me, Expression.Constant(true))))
            {
                return true;
            }
        }
        else if (expression is UnaryExpression ue
            && (ue.NodeType == ExpressionType.Not || ue.NodeType == ExpressionType.Negate)
            && ue.Type == typeof(bool)
            && typeof(TValue) == typeof(bool))
        {
            var expected = false;
            var te = ue.Operand;
            while (te is UnaryExpression ute)
            {
                expected = !expected;
                te = ute.Operand;
            }
            if (IsAlwaysMet(parameter, Expression.Equal(te, Expression.Constant(expected))))
            {
                return true;
            }
        }
        else if (expression is MethodCallExpression mce
            && mce.Method.Name == nameof(ICollection<object>.Contains)
            && mce.Arguments.Count >= 1
            && mce.Arguments.Count <= 2
            && (mce.Arguments.ElementAtOrDefault(mce.Arguments.Count - 2) ?? mce.Object) is ConstantExpression le
            && le.Value is ICollection col
            && col.Count > 0
            && ValueMatcher.IsAlwaysMet(parameter, mce.Arguments.Last())
            && TestingComparison is var predicate
            && Converter is var conv
            && TestingOperand is var mop
            && col.Cast<object>().All(o => conv.TryConvert(o, out var v) && ExpressionTypeHelper.IsAlwaysMet(predicate, ExpressionType.Equal, conv.Compare(v, mop))))
        {
            return true;
        }
        return false;
    }
}
