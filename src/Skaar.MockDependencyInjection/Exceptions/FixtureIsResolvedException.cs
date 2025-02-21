namespace Skaar.MockDependencyInjection.Exceptions;

public class FixtureIsResolvedException<T>(T resolved) 
    : IoCException("Fixture is already resolved. Further setup is not allowed after this point.")
{
    public T Resolved { get; } = resolved;
}