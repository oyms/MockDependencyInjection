using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Resolving;

namespace Skaar.MockDependencyInjection.NSubstitute;

public class ServiceProvider : ServiceProvider<ServiceProvider>
{
    protected override IArgumentResolver CreateArgumentResolver(Type serviceType)
    {
        var key = new ResolverSpecification(serviceType, null);
        return new NSubstituteArgumentResolver(key);
    }

    /// <summary>
    /// Create a fake object for the specified type <typeparamref name="T"/>
    /// and add it to the service collection.
    /// </summary>
    public T AddService<T>() where T : class
    {
        var key = ResolverSpecification.New<T>();
        if (Resolvers[key] is not NSubstituteArgumentResolver resolver)
        {
            resolver = new NSubstituteArgumentResolver(key);
            AddResolver(resolver);
        }
        return (T)resolver.Resolve();
    }

    /// <summary>
    /// Creates a fixture for the specified type <typeparamref name="T"/>
    /// and adds it to the service collection.
    /// </summary>
    public Fixture<T> AddServiceAsFixture<T>() where T : class
    {
        var fixture = new Fixture<T>();
        var resolver = new ResolvableResolver(ResolverSpecification.New<T>(), fixture);
        AddResolver(resolver);
        return fixture;
    }
}