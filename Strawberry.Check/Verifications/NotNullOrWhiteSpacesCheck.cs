using System;
using System.Linq.Expressions;

namespace Strawberry.Check.Verifications
{
    public class NotNullOrWhiteSpacesCheck<TClass> : BaseCheck<TClass, string>
    {
        private readonly string _name;

        public NotNullOrWhiteSpacesCheck(TClass obj, Expression<Func<TClass, string>> expression, string name)
            : base(obj, expression)
        {
            _name = name;
        }

        protected override string ErrorMessage() => $"Value of property {_name} can not be null or contain white spaces.";

        protected override bool Verification(string value) => string.IsNullOrWhiteSpace(value);
    }
}
