using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

class ServiceContainerCollection : ServiceContainer
{
    private List<ServiceContainer> _services = new();

    public void Add(ServiceContainer serviceContainer)
    {
        _services.Add(serviceContainer);
    }
        
    public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
    {
        foreach (var container in _services)
        {
            if (container.TryResolve(type, out instance))
            {
                return true;
            }
        }

        instance = null;
        return false;
    }
}