using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.Resolving;

/// <summary>
/// A resolver that resolves to <paramref name="resolvable"/>.
/// </summary>
/// <param name="key">The parameter specification</param>
/// <param name="resolvable">The object holding the value</param>
public class ResolvableResolver(ResolverSpecification key, IResolvable resolvable): IArgumentResolver
{
    public ResolverSpecification Key { get; } = key;
    public object Resolve() => resolvable.Resolve();
}