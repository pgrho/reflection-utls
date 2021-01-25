using System;
using System.Linq.Expressions;
using System.Text;

namespace Shipwreck.ReflectionUtils
{
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
    }
}
