using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.Resolving;

class InstanceArgumentResolver(ResolverSpecification key, object instance) : IArgumentResolver
{
    public static IArgumentResolver From<T>(T instance, string? parameterName = null) where T: notnull
    {
        return new InstanceArgumentResolver(ResolverSpecification.New<T>(parameterName), instance);
    }

    public ResolverSpecification Key { get; } = key;
    public object Resolve() => instance;
}