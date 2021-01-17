using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

using Strawberry.Check.Interfaces;

namespace Strawberry.Check
{
    public class CheckBuilder<TClass>
        where TClass : class
    {
        private readonly List<ICheck> _verifications;

        public TClass Obj { get; }

        public CheckBuilder(TClass obj, string name)
        {
            Check.NotNull(obj, nameof(obj));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            _verifications = new List<ICheck>();
            Obj = obj!;
        }

        public void AddVerification(ICheck check) =>
            _verifications.Add(check);

        public IReadOnlyCollection<Exception> Exceptions()
        {
            return _verifications.Select(s => s.GetException()).Where(w => w != null).ToList()!;
        }

        public void Verify()
        {
            var exceptions = Exceptions();
            var isSingle = exceptions.Count == 1;
            if (isSingle)
            {
                ExceptionDispatchInfo.Capture(exceptions.First()).Throw();
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
