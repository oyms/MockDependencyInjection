using Moq;
using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Resolving;

namespace Skaar.MockDependencyInjection.Moq;

public class ServiceProvider : Skaar.MockDependencyInjection.ServiceProvider<ServiceProvider>
{
    protected override IArgumentResolver CreateArgumentResolver(Type serviceType)
    {
        var key = new ResolverSpecification(serviceType, null);
        return new MockArgumentResolver(key, new MoqConfig(defaultValueProvider: DefaultValueProvider.Mock));
    }

    /// <summary>
    /// Create a mock object for the specified type <typeparamref name="T"/>
    /// and add it to the service collection.
    /// </summary>
    public Mock<T> AddService<T>(
        MockBehavior behavior = MockBehavior.Loose,
        bool callBase = true,
        DefaultValueProvider? defaultValueProvider = null
        ) where T : class
    {
        var key = ResolverSpecification.New<T>();
        if (Resolvers[key] is not MockArgumentResolver resolver)
        {
            var config = new MoqConfig(behavior, callBase, defaultValueProvider);
            resolver = new MockArgumentResolver(key, config);
            AddResolver(resolver);
        }
        return (Mock<T>)resolver.Mock;
    }

    /// <summary>
    /// Creates a fixture for the specified type <typeparamref name="T"/>
    /// and adds it to the service collection.
    /// </summary>
    public Fixture<T> AddServiceAsFixture<T>() where T : class
    {
        var key = ResolverSpecification.New<T>();
        var fixture = new Fixture<T>();
        var resolver = new ResolvableResolver(ResolverSpecification.New<T>(), fixture);
        AddResolver(resolver);
        return fixture;
    }
}