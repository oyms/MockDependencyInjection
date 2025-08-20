using Skaar.MockDependencyInjection.Exceptions;
using Skaar.MockDependencyInjection.Resolving;

namespace Skaar.MockDependencyInjection.Extensions;

public static class ServiceContainerExtensions
{
    /// <summary>
    /// A convenience method for the <see cref="ServiceContainer.TryResolve"/> method.
    /// </summary>
    /// <exception cref="TypeCannotBeCreatedException">Throws when resolving fails.</exception>
    public static T ResolveOrThrow<T>(this ServiceContainer resolver)
    {
        if (!resolver.TryResolve(typeof(T), out var instance))
        {
            throw new TypeCannotBeCreatedException("Cannot resolve type: " + typeof(T).FullName);
        }

        return (T) instance;
    }
}