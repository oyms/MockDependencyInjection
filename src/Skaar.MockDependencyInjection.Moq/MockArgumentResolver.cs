using Moq;
using Skaar.MockDependencyInjection.Contracts;
using System.Diagnostics;

namespace Skaar.MockDependencyInjection.Moq;

[DebuggerDisplay("{Key}")]
class MockArgumentResolver(ResolverSpecification key, MoqConfig config = default) : IArgumentResolver
{
    private Mock?  _mock;
    public Mock Mock => _mock ??= CreateMock(Key.ArgumentType);
    public object Resolve() => Mock.Object;
    public ResolverSpecification Key { get; } = key;
    private Mock CreateMock(Type t)
    {
        var genericType = typeof(Mock<>).MakeGenericType(t);
        var mock = (Mock)Activator.CreateInstance(genericType, [config.Behavior])!;
        mock.CallBase = config.CallBase;
        mock.DefaultValueProvider = config.DefaultValueProvider ?? DefaultValueProvider.Mock;
        return mock;
    }
}
