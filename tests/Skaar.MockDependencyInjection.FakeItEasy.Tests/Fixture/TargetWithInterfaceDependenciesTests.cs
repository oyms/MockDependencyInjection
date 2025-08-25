using Shouldly;

namespace Skaar.MockDependencyInjection.FakeItEasy.Tests.Fixture;

public class TargetWithInterfaceDependenciesTests
{
    [Fact]
    public void Resolve_WithoutSetup_ReturnsInstance()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var result = fixture.Resolve();

        result.ShouldNotBeNull();
    }
        
    [Fact]
    public void Resolve_WithSetup_DependenciesAreSameAsSetups()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var dep0 = fixture.Arg<IDependency0>();
        var dep1 = fixture.Arg<IDependency1>(optionsBuilder: opt =>
        {
            opt.Named("Some name");
        });
            
        var result = fixture.Resolve();

        result.Dep0.ShouldBe(dep0);
        result.Dep1.ShouldBe(dep1);

        result.Dep0.GetType().Name.ShouldStartWith("ObjectProxy");
    }    
    

    public interface IDependency0
    {
        string Text { get; set; }
    }

    public interface IDependency1
    {
        IDependency0 SomeProperty { get; }
    }
}

file class TestTarget(
    TargetWithInterfaceDependenciesTests.IDependency0 dep0,
    TargetWithInterfaceDependenciesTests.IDependency1 dep1)
{
    public TargetWithInterfaceDependenciesTests.IDependency0 Dep0 { get; init; } = dep0;
    public TargetWithInterfaceDependenciesTests.IDependency1 Dep1 { get; init; } = dep1;
}