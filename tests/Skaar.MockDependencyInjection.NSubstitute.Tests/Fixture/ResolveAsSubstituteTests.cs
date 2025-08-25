using NSubstitute;
using Shouldly;
using System.Collections;

namespace Skaar.MockDependencyInjection.NSubstitute.Tests.Fixture;

public class ResolveAsSubstituteTests
{
    [Fact]
    public void ResolveAsSubstitute_SetupForVirtualMethod_IsVerifiable()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var dependency = fixture.Arg<IDependency>();
        var target = fixture.ResolveAsSubstitute();
       target.VirtualMethod(Arg.Any<string>()).Returns(true);
        
        target.Dep.ShouldBeSameAs(dependency);
        target.VirtualMethod("test").ShouldBeTrue();
        target.Received(1).VirtualMethod("test");
    }

    public class TestTarget(IDependency dep, IEnumerable _)
    {
        public IDependency Dep { get; } = dep;

        public virtual bool VirtualMethod(string arg)
        {
            throw new NotImplementedException();
        }
    }
}

public interface IDependency;