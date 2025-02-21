using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

/// <summary>
/// Provides services (instances) on demand.
/// </summary>
/// <remarks>
/// This may be used as a DI container, providing arguments, when no setup is available.
/// </remarks>
public abstract class ServiceContainer
{
    /// <summary>
    /// Tries to resolve <paramref name="type"/> into a value.
    /// </summary>
    /// <param name="type">The parameter type.</param>
    /// <param name="instance">The instance, if it is resolvable.</param>
    /// <returns><c>true</c> when an instance is resolved.</returns>
    public abstract bool TryResolve(Type type, [NotNullWhen(true)]out object? instance);
}