namespace Skaar.MockDependencyInjection.Exceptions;

/// <summary>
/// The base type of all the exceptions thrown from the fixture.
/// </summary>
public abstract class IoCException: Exception
{
    protected IoCException(string message) : base(message)
    {
    }

    protected IoCException(string message, Exception innerException) : base(message, innerException)
    {
    }
}