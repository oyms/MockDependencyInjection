using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

public abstract class ServiceContainer
{
    public abstract bool TryResolve(Type type, [NotNullWhen(true)]out object? instance);
}