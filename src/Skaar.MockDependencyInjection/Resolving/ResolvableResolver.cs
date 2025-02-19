using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.Resolving;

public class ResolvableResolver(ResolverSpecification key, IResolvable resolvable): IArgumentResolver
{
    public ResolverSpecification Key { get; } = key;
    public object Resolve() => resolvable.Resolve();
}