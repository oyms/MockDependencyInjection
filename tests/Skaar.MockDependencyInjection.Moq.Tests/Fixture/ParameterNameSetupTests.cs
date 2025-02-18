using Shouldly;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture
{
    public class ParameterNameSetupTests
    {
        [Fact]
        public void Arg_WithParameterName_SetsSpecificParameter()
        {
            var fixture = IoC.CreateFixture<TestTarget>();
            var first = fixture.Arg<IDependency>(parameterName: "first");
            var second = fixture.Arg(new Implementation(),  parameterName: "second");
            var third = fixture.Arg<IDependency>();

            var result = fixture.Resolve();
            
            result.First.ShouldBeSameAs(first.Object);
            result.Second.ShouldBeSameAs(second);
            result.Third.ShouldBeSameAs(third.Object);
        }
        
        public interface IDependency;
    }

    file class TestTarget(
        ParameterNameSetupTests.IDependency first, 
        ParameterNameSetupTests.IDependency second,
        ParameterNameSetupTests.IDependency third)
    {
        public ParameterNameSetupTests.IDependency First { get; } = first;
        public ParameterNameSetupTests.IDependency Second { get; } = second;
        public ParameterNameSetupTests.IDependency Third { get; } = third;
    }
    file record Implementation:ParameterNameSetupTests.IDependency;
}