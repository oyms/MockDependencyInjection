using Shouldly;
using Skaar.MockDependencyInjection.Resolving;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture
{
    public class OtherTypesWithDefaultConstructorsAsParametersTests
    {
        [Fact]
        public void Resolve_ParameterIsClassWithDefaultConstructor_CreatesInstance()
        {
            var fixture = IoC.CreateFixture<TestTarget>().Use(new ValueTypeGenerator());
            
            var result = fixture.Resolve();

            result.ShouldNotBeNull();
        }

        public record RecordWithDefaultConstructor;
        public struct StructWithDefaultConstructor;
        public abstract class AbstractClassWithDefaultConstructor;
    }

    file class TestTarget(
        OtherTypesWithDefaultConstructorsAsParametersTests.RecordWithDefaultConstructor  first,
        OtherTypesWithDefaultConstructorsAsParametersTests.StructWithDefaultConstructor second,
        OtherTypesWithDefaultConstructorsAsParametersTests.AbstractClassWithDefaultConstructor third,
        bool fourth,
        string fifth
        )
    {
        public OtherTypesWithDefaultConstructorsAsParametersTests.RecordWithDefaultConstructor First { get; } = first;
        public OtherTypesWithDefaultConstructorsAsParametersTests.StructWithDefaultConstructor Second { get; } = second;
        public OtherTypesWithDefaultConstructorsAsParametersTests.AbstractClassWithDefaultConstructor Third { get; } = third;
        public bool Fourth { get; } = fourth;
        public string Fifth { get; } = fifth;
    }
}