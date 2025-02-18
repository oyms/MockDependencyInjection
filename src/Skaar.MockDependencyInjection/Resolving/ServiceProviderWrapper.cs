using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving
{
    class ServiceProviderWrapper(IServiceProvider services) : ServiceContainer
    {
        public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
        {
            instance = services.GetService(type);
            return instance != null;
        }
    }
}
