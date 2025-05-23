using FakeItEasy;
using Shouldly;

namespace Skaar.MockDependencyInjection.FakeItEasy.Tests.Fixture;

public class ArgumentAsFixtureTests
{
    [Fact]
    public void ArgAsFixture_ForParameter_ReturnsNewFixture()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var subFixture = fixture.ArgAsFixture<OtherClass>();
        var arg = subFixture.Arg<IDependency1>();
        A.CallTo(() => arg.Text).Returns("Expected text");

        var result = fixture.Resolve();
        
        result.Other.Second.Text.ShouldBe("Expected text");
    }
    public interface IDependency0;

    public interface IDependency1
    {
        string Text { get; }
    };
}

#pragma warning disable CS9113 // Parameter is unread.
// ReSharper disable ClassNeverInstantiated.Local
file class TestTarget(ArgumentAsFixtureTests.IDependency0 first, ArgumentAsFixtureTests.IDependency1 second, OtherClass other)
{
    public OtherClass Other { get; } = other;
}

file class OtherClass(ArgumentAsFixtureTests.IDependency0 first, ArgumentAsFixtureTests.IDependency1 second)
{
    public ArgumentAsFixtureTests.IDependency1 Second { get; } = second;
}