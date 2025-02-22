using AutoFixture.Kernel;
using Skaar.MockDependencyInjection.Resolving;
using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.AutoFixture;

/// <summary>
/// This <see cref="Resolving.ServiceContainer"/> will use
/// <see cref="global::AutoFixture.Fixture"/> to create instance
/// of types that are not interfaces.
/// </summary>
/// <example>
///  <c>var fixture = IoC.CreateFixture().Use(autoFixtureProvider);</c>
/// </example>
/// <remarks>
/// Use <see cref="Fixture"/> to customize the instance creation.
/// </remarks>
public class AutoFixtureProvider : ServiceContainer
{
    /// <summary>
    /// The fixture used to create instances.
    /// </summary>
    public global::AutoFixture.Fixture Fixture { get; } = new(); 

    /// <inheritdoc cref="ServiceContainer.TryResolve"/>
    /// <remarks>
    /// Will not work with interface types.
    /// </remarks>
    public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
    {
        if (type.IsInterface)
        {
            instance = null;
            return false;
        }
        try
        {
            var context = new SpecimenContext(Fixture);
            instance = context.Resolve(type);
            return instance != null;
        }
        catch
        {
            instance = null;
            return false;
        }
    }
}