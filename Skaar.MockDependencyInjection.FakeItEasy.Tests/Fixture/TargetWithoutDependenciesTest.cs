using Shouldly;

namespace Skaar.MockDependencyInjection.FakeItEasy.Tests.Fixture;

public class TargetWithoutDependenciesTests
{
    [Fact]
    public void Resolve_WithoutSetup_ReturnsInstance()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var result = fixture.Resolve();

        result.ShouldNotBeNull();
    }

    [Fact]
    public void Resolve_MultipleCalls_ReturnsSameInstance()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
            
        var result0 = fixture.Resolve();
        var result1 = fixture.Resolve();
            
        result0.ShouldBeSameAs(result1);
    }
}
file class TestTarget;