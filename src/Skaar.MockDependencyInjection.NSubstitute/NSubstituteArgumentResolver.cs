using NSubstitute;
using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.NSubstitute;

class NSubstituteArgumentResolver(ResolverSpecification key) : IArgumentResolver
{
    public ResolverSpecification Key { get; } = key;
    private object? _mock;
    public object Resolve()
    {
        return _mock ??= Substitute.For([Key.ArgumentType], []);
    }
}