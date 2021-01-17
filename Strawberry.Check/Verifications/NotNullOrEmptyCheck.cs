using System;
using System.Linq.Expressions;

namespace Strawberry.Check.Verifications
{
    public class NotNullOrEmptyCheck<TClass> : BaseCheck<TClass, string>
    {
        private readonly string _name;

        public NotNullOrEmptyCheck(TClass obj, Expression<Func<TClass, string>> expression, string name)
            : base(obj, expression)
        {
            _name = name;
        }

        protected override string ErrorMessage() => $"Value of property {_name} can not be null or empty.";

        protected override bool Verification(string value) => string.IsNullOrEmpty(value);
    }
}
