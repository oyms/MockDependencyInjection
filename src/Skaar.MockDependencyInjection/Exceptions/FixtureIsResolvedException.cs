namespace Skaar.MockDependencyInjection.Exceptions
{
    public class FixtureIsResolvedException<T> : IoCException
    {
        public T Resolved { get; }

        public FixtureIsResolvedException(T resolved) : base("Fixture is already resolved. Further setup is not allowed after this point.")
        {
            Resolved = resolved;
        }
    }
}