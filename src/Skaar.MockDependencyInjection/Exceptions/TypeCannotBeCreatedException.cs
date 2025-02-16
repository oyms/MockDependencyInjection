namespace Skaar.MockDependencyInjection.Exceptions
{
    public class TypeCannotBeCreatedException(string message) : IoCException(message);
}