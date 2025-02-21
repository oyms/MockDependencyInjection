using NSubstitute;
using Shouldly;

namespace Skaar.MockDependencyInjection.NSubstitute.Tests.Fixture;

public class ArgumentAsFixtureTests
{
    [Fact]
    public void ArgAsFixture_ForParameter_ReturnsNewFixture()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var subFixture = fixture.ArgAsFixture<OtherClass>();
        var arg = subFixture.Arg<IDependency1>();
        arg.Text.Returns("Expected text");

        var result = fixture.Resolve();
        
        result.Other.Second.Text.ShouldBe("Expected text");
    }
    public interface IDependency0;

    public interface IDependency1
    {
        string Text { get; }
    };
}

file class TestTarget(ArgumentAsFixtureTests.IDependency0 first, ArgumentAsFixtureTests.IDependency1 second, OtherClass other)
{
    public OtherClass Other { get; } = other;
}

file class OtherClass(ArgumentAsFixtureTests.IDependency0 first, ArgumentAsFixtureTests.IDependency1 second)
{
    public ArgumentAsFixtureTests.IDependency1 Second { get; } = second;
}