using Moq;
using Shouldly;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

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
        var dep1 = fixture.Arg<IDependency1>();
            
        var result = fixture.Resolve();

        result.Dep0.ShouldBe(dep0.Object);
        result.Dep1.ShouldBe(dep1.Object);
    }    
        
    [Fact]
    public void Resolve_WithSetupWithMockBehaviour_SetsBehaviourOtherwiseDefault()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var dep0 = fixture.Arg<IDependency0>();
        var dep1 = fixture.Arg<IDependency1>(
            behavior: MockBehavior.Strict, 
            callBase: false,
            defaultValueProvider: DefaultValueProvider.Empty);
            
        _ = fixture.Resolve();

        dep0.Behavior.ShouldBe(MockBehavior.Loose);
        dep0.CallBase.ShouldBe(true);
        dep0.DefaultValueProvider.ShouldBe(DefaultValueProvider.Mock);
            
        dep1.Behavior.ShouldBe(MockBehavior.Strict);
        dep1.CallBase.ShouldBe(false);
        dep1.DefaultValueProvider.ShouldBe(DefaultValueProvider.Empty);
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