namespace Skaar.MockDependencyInjection.Exceptions
{
    public class FixtureIsResolvedException<T>(T Resolved) 
        : IoCException("Fixture is already resolved. Further setup is not allowed after this point.");
}