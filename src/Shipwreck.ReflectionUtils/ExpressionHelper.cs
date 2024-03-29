﻿namespace Shipwreck.ReflectionUtils;

public static class ExpressionHelper
{
    public static string GetMemberPath<T, TProperty>(this Expression<Func<T, TProperty>> expression)
    {
        StringBuilder sb = null;
        for (var e = expression.Body as MemberExpression; e != null; e = e.Expression as MemberExpression)
        {
            if (sb == null)
            {
                sb = new StringBuilder(e.Member.Name);
            }
            else
            {
                sb.Insert(0, e.Member.Name);
                sb.Insert(e.Member.Name.Length, '.');
            }
        }
        return sb?.ToString();
    }

    public static bool IsAlwaysMet(ParameterExpression parameter, Expression condition, Func<ParameterExpression, Expression, bool> predicate)
    {
        if (predicate(parameter, condition))
        {
            return true;
        }

        if (condition is BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return IsAlwaysMet(parameter, b.Left, predicate) || IsAlwaysMet(parameter, b.Right, predicate);

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return IsAlwaysMet(parameter, b.Left, predicate) && IsAlwaysMet(parameter, b.Right, predicate);
            }
        }

        return false;
    }

    public static bool TryGetConstant(this Expression expression, out object result)
    {
        if (expression is ConstantExpression c)
        {
            result = c.Value;
            return true;
        }
        if (expression is MemberExpression m)
        {
            if (m.Expression is ConstantExpression mec)
            {
                if (mec.Type.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                {
                    if (m.Member is PropertyInfo p)
                    {
                        result = p.GetValue(mec.Value);
                        return true;
                    }
                    if (m.Member is FieldInfo f)
                    {
                        result = f.GetValue(mec.Value);
                        return true;
                    }
                }
            }
        }
        result = null;
        return false;
    }

    private sealed class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression Current;

        private readonly Expression NewValue;

        public ReplaceExpressionVisitor(Expression current, Expression newValue)
        {
            Current = current;
            NewValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node != Current)
            {
                return base.Visit(node);
            }

            return NewValue;
        }
    }

    public static Expression Replace(this Expression expression, Expression current, Expression newValue)
        => new ReplaceExpressionVisitor(current, newValue).Visit(expression);
}
