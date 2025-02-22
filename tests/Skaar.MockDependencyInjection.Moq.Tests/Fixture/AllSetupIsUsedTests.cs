using Shouldly;
using Skaar.MockDependencyInjection.Exceptions;
// ReSharper disable UnusedVariable

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class AllSetupIsUsedTests
{
    [Fact]
    public void Resolve_WhenPropertiesAreSetupThatIsNotUsed_Throws()
    {
        var fixture = IoC.CreateFixture<TestTarget>();
        var a0 = fixture.Arg<I0>();
        var a2 = fixture.Arg<I2>();
        var unused = fixture.Arg<INotUsed>();
        var alsoUnused = fixture.Arg<string>("notUsed", "NotExists");

        Should.Throw<UnusedSetupException>(() => fixture.Resolve());
    }
        
    public interface I0;
    public interface I1;
    public interface I2;
    public interface INotUsed;
}

file class TestTarget(
#pragma warning disable CS9113 // Parameter is unread.
    AllSetupIsUsedTests.I0 first,
    AllSetupIsUsedTests.I1 second,
    AllSetupIsUsedTests.I2 third
);