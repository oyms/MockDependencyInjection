using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

class ServiceProviderWrapper(IServiceProvider services) : ServiceContainer
{
    public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
    {
        try
        {
            instance = services.GetService(type);
            return instance != null;
        }
        catch
        {
            instance = null;
            return false;       
        }
    }
}