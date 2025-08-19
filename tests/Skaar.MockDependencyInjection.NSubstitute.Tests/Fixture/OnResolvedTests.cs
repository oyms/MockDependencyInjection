using Shouldly;

namespace Skaar.MockDependencyInjection.NSubstitute.Tests.Fixture;

public class OnResolvedTests
{
    [Fact]
    public void OnResolved_CallsAction()
    {
        var expected = "Some text";
        var fixture = IoC.CreateFixture<TestTarget>();
        fixture.OnResolved += (_, target) => target.Text = expected;

        var result = fixture.Resolve();
        
        result.Text.ShouldBe(expected);
    }
}

file class TestTarget(ArgumentAsFixtureTests.IDependency0 first)
{
    public string Text { get; set; }
}