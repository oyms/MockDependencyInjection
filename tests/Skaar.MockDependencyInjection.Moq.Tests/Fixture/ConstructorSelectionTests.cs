using Shouldly;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class ConstructorSelectionTests
{
    [Fact]
    public void Resolve_WhenDefaultConstructorCanBeAvoided_OtherConstructorIsUsed()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var result = fixture.Resolve();
        result.Text.ShouldNotBe("default constructor");
    }

    [Fact]
    public void Resolve_WithMultipleConstructorsNotAllArgsAreSetup_UsesBestMatchForSetup()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        fixture.Arg<IDependency1>();
        
        var result = fixture.Resolve();
        
        result.Text.ShouldBe("constructor with one argument");
    }
    
    [Fact]
    public void Resolve_WithMultipleConstructors_UsesBestMatchForSetup()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        fixture.Arg<IDependency0>();
        fixture.Arg<IDependency1>();
        
        var result = fixture.Resolve();
        
        result.Text.ShouldBe("constructor with three arguments");
    }

    public interface IDependency0;
    public interface IDependency1;
    public interface IDependency2;
}

file class TestTarget
{
    public TestTarget()
    {
        Text = "default constructor";
    }

    public TestTarget(ConstructorSelectionTests.IDependency1 dep1)
    {
        Text = "constructor with one argument";
    }
    
    public TestTarget(ConstructorSelectionTests.IDependency0 dep0, ConstructorSelectionTests.IDependency1 dep1,  ConstructorSelectionTests.IDependency2 dep2)
    {
        Text = "constructor with three arguments";
    }

    public string Text { get; }
}