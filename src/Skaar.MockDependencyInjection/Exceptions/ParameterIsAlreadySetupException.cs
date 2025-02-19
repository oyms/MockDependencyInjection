using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.Exceptions;

public class ParameterIsAlreadySetupException(ResolverSpecification key) : IoCException(
    $"The parameter is already setup ({key})")
{
    public ResolverSpecification Key { get; } = key;
}