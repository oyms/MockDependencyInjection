using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
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

    /// <summary>
    /// When the target has dependency on <see cref="ILogger{TCategoryName}"/>
    /// or <see cref="ILogger{TCategoryName}"/>, this method can be used to inject
    /// a fake logger <seealso cref="FakeLogger"/> to direct the log output.
    /// </summary>
    /// <param name="fixture">The fixture to add this to.</param>
    /// <param name="sink">
    /// The output to write the log messages to.
    /// Defaults to <see cref="Console"/>.
    /// </param>
    /// <returns>This fixture</returns>
    /// <remarks>
    /// When using xUnit, use <c>ITestOutputHelper.WriteLine</c> as <paramref name="sink"/>,
    /// to get the log output in the test results.
    /// </remarks>
    public static TFixture UseLogSink<T, TFixture>(this Fixture<T, TFixture> fixture, Action<string>? sink = null) where T : class where TFixture : Fixture<T, TFixture>
    {
        return fixture.Use(new MicrosoftLoggerResolver(sink ?? Console.Out.WriteLine));
    }
}