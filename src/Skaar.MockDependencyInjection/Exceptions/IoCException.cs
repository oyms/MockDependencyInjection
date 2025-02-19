namespace Skaar.MockDependencyInjection.Exceptions;

public abstract class IoCException: Exception
{
    protected IoCException(string message) : base(message)
    {
    }

    protected IoCException(string message, Exception innerException) : base(message, innerException)
    {
    }
}