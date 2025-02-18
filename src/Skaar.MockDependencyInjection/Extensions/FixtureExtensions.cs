using Skaar.MockDependencyInjection.Resolving;
using System.ComponentModel.Design;

namespace Skaar.MockDependencyInjection.Extensions
{
    public static class FixtureExtensions
    {
        public static TFixture Use<T, TFixture>(this Fixture<T, TFixture> fixture ,IServiceContainer serviceContainer) where T : class where TFixture : Fixture<T, TFixture>
        {
            fixture.Use(new ServiceProviderWrapper(serviceContainer));
            return (TFixture) fixture;
        }
    }
}