using System;
using System.Linq.Expressions;

namespace Strawberry.Check.Verifications
{
    public class IsEnumValueDefinedCheck<TClass, TProp> : BaseCheck<TClass, TProp>
       where TProp : Enum
    {
        private readonly string _name;

        public IsEnumValueDefinedCheck(TClass obj, Expression<Func<TClass, TProp>> expression, string name)
           : base(obj, expression)
        {
            _name = name;
        }

        protected override bool Verification(TProp value) => !Enum.IsDefined(typeof(TProp), value);

        protected override string ErrorMessage() => string.Empty;

        protected override Exception CreateException() => new ArgumentOutOfRangeException(_name);
    }
}
