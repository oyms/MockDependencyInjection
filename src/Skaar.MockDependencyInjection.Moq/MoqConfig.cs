using Moq;

namespace Skaar.MockDependencyInjection.Moq;

struct MoqConfig(
    MockBehavior behavior = MockBehavior.Loose,
    bool callBase = true,
    DefaultValueProvider? defaultValueProvider = null)
{
    public MockBehavior Behavior { get; } = behavior;
    public bool CallBase { get; } = callBase;
    public DefaultValueProvider? DefaultValueProvider { get; } = defaultValueProvider ?? DefaultValueProvider.Mock;
}