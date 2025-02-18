using AutoFixture;
using Shouldly;
using Skaar.MockDependencyInjection.AutoFixture;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture
{
    public class AutoFixtureProviderTests
    {
        [Fact]
        public void Resolve_WithAutoFixtureProvider_ReturnsInstance()
        {
            var autoFixtureProvider = new AutoFixtureProvider();
            autoFixtureProvider.Fixture.OmitAutoProperties = false;
            autoFixtureProvider.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var fixture = IoC.CreateFixture<TestTarget>()
                .Use(autoFixtureProvider);

            var result = fixture.Resolve();
            
            result.ShouldNotBeNull();
        }

        public interface IDependency;

        public sealed class SomeClass
        {
            public required OtherClass Other { get; set; }
        }

        public class OtherClass(string value)
        {
            public string Value { get; } = value;
        }
    }

    file class TestTarget(
        AutoFixtureProviderTests.IDependency first,
        string second,
        double third,
        AutoFixtureProviderTests.SomeClass recursiveFourth
        )
    {
        public AutoFixtureProviderTests.IDependency First { get; } = first;
        public string Second { get; } = second;
        public double Third { get; } = third;
        public AutoFixtureProviderTests.SomeClass RecursiveFourth { get; } = recursiveFourth;
    }
}