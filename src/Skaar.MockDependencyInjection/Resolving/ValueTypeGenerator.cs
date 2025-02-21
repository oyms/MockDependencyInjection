using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

/// <summary>
/// A value type provider. Can resolve value types to default instances.
/// </summary>
/// <remarks>Can also generate string values.</remarks>
public class ValueTypeGenerator: ServiceContainer
{
    /// <summary>
    /// Tries to generate a value
    /// </summary>
    /// <param name="type">
    /// The type to generate an instance of. It must be a value type or a string
    /// for a value to be rendered.
    /// </param>
    /// <param name="instance">The value resolved.</param>
    /// <returns><c>true</c> if a value was provided.</returns>
    /// <remarks>
    /// <paramref name="type"/> must be <see cref="Type.IsValueType"/> or of
    /// type <see cref="String"/>.
    /// </remarks>
    public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
    {
        if (type.IsValueType)
        {
            instance = Activator.CreateInstance(type)!;
            return true;
        }

        if (type == typeof(string))
        {
            instance = Guid.NewGuid().ToString();
            return true;
        }

        instance = null;
        return false;
    }
}