using Shouldly;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture
{
    public class TargetWithoutDependenciesTests
    {
        [Fact]
        public void Resolve_WithoutSetup_ReturnsInstance()
        {
            var fixture = IoC.CreateFixture<TestTarget>();
            var result = fixture.Resolve();

            result.ShouldNotBeNull();
        }
    }
    file class TestTarget{}
}