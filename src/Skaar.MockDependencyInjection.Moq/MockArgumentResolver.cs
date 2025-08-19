using Moq;
using Skaar.MockDependencyInjection.Contracts;
using System.Diagnostics;

namespace Skaar.MockDependencyInjection.Moq;

[DebuggerDisplay("{Key}")]
class MockArgumentResolver(ResolverSpecification key, MoqConfig? config = null) : IArgumentResolver
{
    private Mock?  _mock;
    public Mock Mock => _mock ??= CreateMock(Key.ArgumentType);
    public object Resolve() => Mock.Object;
    private MoqConfig Config => config.GetValueOrDefault(new MoqConfig(behavior: MockBehavior.Loose, defaultValueProvider: DefaultValueProvider.Mock));
    public ResolverSpecification Key { get; } = key;
    private Mock CreateMock(Type t)
    {
        var genericType = typeof(Mock<>).MakeGenericType(t);
        var mock = (Mock)Activator.CreateInstance(genericType, [Config.Behavior])!;
        mock.CallBase = Config.CallBase;
        mock.DefaultValueProvider = Config.DefaultValueProvider ?? DefaultValueProvider.Mock;
        return mock;
    }
}
