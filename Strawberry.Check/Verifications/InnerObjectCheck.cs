using System;
using System.Linq;
using System.Linq.Expressions;

using Strawberry.Check.Extensions;
using Strawberry.Check.Interfaces;

namespace Strawberry.Check.Verifications
{
    public class InnerObjectCheck<TClass, TInner> : ICheck
        where TInner : class
    {
        private readonly TClass _obj;

        private readonly Expression<Func<TClass, TInner>> _expression;

        private readonly Action<CheckBuilder<TInner>> _verifications;

        private readonly string _name;

        public InnerObjectCheck(
            TClass obj,
            Expression<Func<TClass, TInner>> expression,
            Action<CheckBuilder<TInner>> verifications,
            string name)
        {
            _obj = obj;
            _expression = expression;
            _verifications = verifications;
            _name = name;
        }

        public Exception? GetException()
        {
            var result = _expression.GetValue(_obj);
            if (result == default(TInner))
            {
                return new ArgumentNullException(_name);
            }

            var builder = new CheckBuilder<TInner>(result, _name);
            try
            {
                _verifications(builder);
            }
            catch (Exception ex)
            {
                return ex;
            }

            var exceptions = builder.Exceptions();
            if (exceptions.Any())
            {
                return new AggregateException(exceptions);
            }

            return null;
        }
    }
}
