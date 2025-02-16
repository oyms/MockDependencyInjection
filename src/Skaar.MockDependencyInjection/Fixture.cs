using Skaar.MockDependencyInjection.Exceptions;

namespace Skaar.MockDependencyInjection
{
    public abstract class Fixture<T> where T : class
    {
        private T? _resolved;
        protected abstract T CreateInstance();

        public T Resolve() => _resolved = CreateInstance();

        protected void AssertNotResolved()
        {
            if (_resolved is not null)
            {
                throw new FixtureIsResolvedException<T>(_resolved);
            }
        }
    }
}