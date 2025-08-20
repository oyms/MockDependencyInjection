using Moq;
using Shouldly;
using System.Collections;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class ResolveAsMockTests
{
    [Fact]
    public void ResolveAsMock_SetupForVirtualMethod_IsVerifiable()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var dependency = fixture.Arg<InstanceSetupTests.IDependency>();
        var target = fixture.ResolveAsMock(callBase:true);
        target.Setup(x => x.VirtualMethod(It.IsAny<string>())).Returns(true);
        
        target.Object.Dep.ShouldBeSameAs(dependency.Object);
        target.Object.VirtualMethod("test").ShouldBeTrue();
        target.Verify(x => x.VirtualMethod("test"), Times.Once);
    }
    public class TestTarget(InstanceSetupTests.IDependency dep, IEnumerable _)
     {
         public InstanceSetupTests.IDependency Dep { get; } = dep;
     
         public virtual bool VirtualMethod(string arg)
         {
             throw new NotImplementedException();
         }
     }
}
