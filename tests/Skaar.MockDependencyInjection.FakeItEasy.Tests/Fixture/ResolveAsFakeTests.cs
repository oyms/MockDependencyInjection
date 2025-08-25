using FakeItEasy;
using Shouldly;
using System.Collections;

namespace Skaar.MockDependencyInjection.FakeItEasy.Tests.Fixture;

public class ResolveAsFakeTests
{
    [Fact]
    public void ResolveAsFake_SetupForVirtualMethod_IsVerifiable()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var dependency = fixture.Arg<IDependency>();
        var target = fixture.ResolveAsFake();
        A.CallTo(() => target.VirtualMethod(A<string>.Ignored)).Returns(true);
        
        target.Dep.ShouldBeSameAs(dependency);
        target.VirtualMethod("test").ShouldBeTrue();
        A.CallTo(() => target.VirtualMethod("test")).MustHaveHappenedOnceExactly();
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