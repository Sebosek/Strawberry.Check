using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Strawberry.Check.Verifications
{
    public class NotNullCheck<TClass, TProp> : BaseCheck<TClass, TProp>
        where TProp : class
    {
        private readonly string _name;

        public NotNullCheck([AllowNull] TClass obj, Expression<Func<TClass, TProp>> expression, string name)
           : base(obj, expression)
        {
            _name = name;
        }

        protected override bool Verification(TProp value) => value == default(TProp);

        protected override string ErrorMessage() => string.Empty;

        protected override Exception CreateException() => new ArgumentNullException(_name);
    }
}
