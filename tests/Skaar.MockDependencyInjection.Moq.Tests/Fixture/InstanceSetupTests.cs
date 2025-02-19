using Shouldly;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class InstanceSetupTests
{
    [Fact]
    public void Arg_WithInstances_InjectsInstanceInTarget()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var expected = new Implementation { Text = "Some text" };

        var arg = fixture.Arg(expected);
        var result = fixture.Resolve();
            
        arg.ShouldBeSameAs(expected);
        result.Dep.ShouldBeSameAs(expected);
    }
    public interface IDependency
    {
        string Text { get; set; }
    }
}
file class TestTarget(InstanceSetupTests.IDependency dep)
{
    public InstanceSetupTests.IDependency Dep { get; } = dep;
}

file class Implementation : InstanceSetupTests.IDependency
{
    public string Text { get; set; }
}