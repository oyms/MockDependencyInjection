using Skaar.MockDependencyInjection.Contracts;

namespace Skaar.MockDependencyInjection.FakeItEasy;

class FakeItEasyArgumentResolver(ResolverSpecification key, object? optionsBuilder = null) : IArgumentResolver
{
    private object? _fake;
    public object Resolve()
    {
        return _fake ??= CreateFake();
    }

    public ResolverSpecification Key { get; } = key;

    private object CreateFake() => FakeItEasyCreator.CreateFake(Key.ArgumentType, optionsBuilder);
}