using System;
using System.Linq.Expressions;

namespace Strawberry.Check.Extensions
{
    public static class ExpressionExtensions
    {
        public static string DeriveName<TParent, TMember>(this Expression<Func<TParent, TMember>> expression)
            where TParent : class
        {
            MemberExpression memberExpression;

            if (expression.Body is MemberExpression body)
            {
                memberExpression = body;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                memberExpression = (MemberExpression)op;
            }

            var property = memberExpression.Member;
            return property.Name;
        }

        public static TMember? GetValue<TParent, TMember>(
            this Expression<Func<TParent, TMember>> expression,
            TParent parent)
        {
            if (Equals(parent, default(TParent)))
            {
                return default;
            }

            return expression.Compile()(parent);
        }
    }
}
