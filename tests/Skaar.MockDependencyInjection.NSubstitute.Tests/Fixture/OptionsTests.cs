using Microsoft.Extensions.Options;
using Shouldly;
using Skaar.MockDependencyInjection.Extensions;

namespace Skaar.MockDependencyInjection.NSubstitute.Tests.Fixture;

public class OptionsTests
{
    [Fact]
    public void Resolve_WithOptions_ResolvesToInstance()
    {
        var fixture = IoC.CreateFixture<TestTarget>();

        fixture.UseOptions(new AppConfig("Expected value"));
        var result = fixture.Resolve();

        result.Config.SomeStringValue.ShouldBe("Expected value");
    }
}

// ReSharper disable once ClassNeverInstantiated.Local
file class TestTarget(IOptions<AppConfig> options)
{
    public AppConfig Config => options.Value;
}

file record AppConfig(string SomeStringValue);