using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using Strawberry.Check.Extensions;
using Strawberry.Check.Interfaces;

namespace Strawberry.Check.Verifications
{
    public abstract class BaseCheck<TClass, TProp> : ICheck
    {
        private TClass? Obj { get; }

        private Expression<Func<TClass?, TProp>> Expression { get; }

        protected BaseCheck([AllowNull] TClass obj, Expression<Func<TClass?, TProp>> expression)
        {
            Obj = obj;
            Expression = expression;
        }

        protected abstract bool Verification(TProp? value);

        protected abstract string ErrorMessage();

        protected virtual Exception CreateException() =>
            new ArgumentException(ErrorMessage());

        public Exception? GetException()
        {
            var result = Expression.GetValue(Obj);
            if (Verification(result))
            {
                return CreateException();
            }

            return null;
        }
    }
}
