using Skaar.MockDependencyInjection.Resolving;
using System.ComponentModel.Design;

namespace Skaar.MockDependencyInjection.Extensions;

public static class FixtureExtensions
{
    /// <summary>
    /// Adds a service container as a <see cref="IServiceProvider"/>
    /// </summary>
    /// <remarks>
    /// The <paramref name="serviceContainer"/> is wrapped in a <see cref="IServiceProvider"/>
    /// and sent to <see cref="Fixture{T,TFixture}.Use"/>.
    /// </remarks>
    public static TFixture Use<T, TFixture>(this Fixture<T, TFixture> fixture ,IServiceContainer serviceContainer) where T : class where TFixture : Fixture<T, TFixture>
    {
        fixture.Use(new ServiceProviderWrapper(serviceContainer));
        return (TFixture) fixture;
    }
}