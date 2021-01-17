using System;
using System.Linq.Expressions;

using Strawberry.Check.Verifications;

namespace Strawberry.Check.Extensions
{
    public static class VerificationBuilderExtensions
    {
        public static CheckBuilder<TClass> NotNull<TClass, TProp>(
            this CheckBuilder<TClass> builder,
            Expression<Func<TClass, TProp>> expression)
            where TClass : class
            where TProp : class
        {
            builder.AddVerification(
                new NotNullCheck<TClass, TProp>(builder.Obj, expression, expression.DeriveName()));

            return builder;
        }

        public static CheckBuilder<TClass> NotNullOrEmpty<TClass>(
            this CheckBuilder<TClass> builder,
            Expression<Func<TClass, string>> expression)
            where TClass : class
        {
            builder.AddVerification(
                new NotNullOrEmptyCheck<TClass>(builder.Obj, expression, expression.DeriveName()));

            return builder;
        }

        public static CheckBuilder<TClass> NotNullOrWhiteSpaces<TClass>(
            this CheckBuilder<TClass> builder,
            Expression<Func<TClass, string>> expression)
            where TClass : class
        {
            builder.AddVerification(
                new NotNullOrWhiteSpacesCheck<TClass>(builder.Obj, expression, expression.DeriveName()));

            return builder;
        }

        public static CheckBuilder<TClass> InnerObject<TClass, TProp>(
            this CheckBuilder<TClass> builder,
            Expression<Func<TClass, TProp>> expression,
            Action<CheckBuilder<TProp>> verifications)
            where TClass : class
            where TProp : class
        {
            builder.AddVerification(
                new InnerObjectCheck<TClass, TProp>(
                    builder.Obj,
                    expression,
                    verifications,
                    expression.DeriveName()));

            return builder;
        }
    }
}
