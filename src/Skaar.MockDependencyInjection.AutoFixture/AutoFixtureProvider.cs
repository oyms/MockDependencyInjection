using AutoFixture;
using AutoFixture.Kernel;
using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.AutoFixture;

public class AutoFixtureProvider : Resolving.ServiceContainer
{
    public global::AutoFixture.Fixture Fixture { get; } = new(); 
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