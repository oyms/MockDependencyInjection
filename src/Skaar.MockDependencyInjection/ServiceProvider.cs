using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Resolving;

namespace Skaar.MockDependencyInjection;

/// <summary>
/// A base class for implementing a service provider.
/// </summary>
public abstract class ServiceProvider<T> : IServiceProvider where T:ServiceProvider<T>
{
    private readonly ServiceContainerCollection _serviceContainers = new();
    
    protected IArgumentResolverCollection Resolvers { get; } = new ArgumentResolverCollection();
    
    /// <summary>
    /// Use this method to provide an Ioc-container/service provider for parameters that is not set up.
    /// </summary>
    /// <param name="serviceContainer">The service container to inject, providing values.</param>
    /// <returns>This provider</returns>
    public T Use(ServiceContainer serviceContainer)
    {
        _serviceContainers.Add(serviceContainer);
        return (T) this;
    }
    protected abstract IArgumentResolver  CreateArgumentResolver(Type serviceType);
    
    protected void AddResolver(IArgumentResolver resolver)
    {
        Resolvers.Add(resolver);
    }

    public T AddService<TS>(TS instance) where TS : notnull
    {
        var resolver = InstanceArgumentResolver.From(instance);
        Resolvers.Add(resolver);
        return (T) this;
    }

    /// <inheritdoc cref="IServiceProvider.GetService"/>
    object IServiceProvider.GetService(Type serviceType)
    {
        if (Resolvers.TryResolve(new ResolverSpecification(serviceType, null), out var resolver))
        {
            return resolver.Resolve();
        }

        if (_serviceContainers.TryResolve(serviceType, out var instance))
        {
            return instance;
        }
        var newResolver = CreateArgumentResolver(serviceType);
        Resolvers.Add(newResolver);
        return newResolver.Resolve();
    }
}