using Shouldly;
using Skaar.MockDependencyInjection.Exceptions;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class InvalidTargetTests
{
    [Fact]
    public void Resolve_FromInterface_Throws()
    {
        var fixture = IoC.CreateFixture<ITestTarget>();
            
        Should.Throw<TypeCannotBeCreatedException>(() => fixture.Resolve());
    }    
        
    [Fact]
    public void Resolve_FromAbstractClass_Throws()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
            
        Should.Throw<TypeCannotBeCreatedException>(() => fixture.Resolve());
    }
    public interface ITestTarget;
    public abstract class TestTarget : ITestTarget;
}