using Shouldly;
using Skaar.MockDependencyInjection.Extensions;
using System.ComponentModel.Design;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class ServiceProviderTests
{
    [Fact]
    public void Resolve_WhenDiContainerIsAdded_NonSetupsAreFetchedFromContainer()
    {
        var implementation = new Implementation("Text 0");
        var services = new ServiceContainer();
        services.AddService(typeof(IDependency), implementation);
        var fixture = IoC.CreateFixture<TestTarget>().Use(services);

        var result = fixture.Resolve();

        result.Dep.ShouldBeSameAs(implementation);
    }

    [Fact]
    public void Resolve_WhenDiContainerIsAdded_SetupsAreUsed()
    {
        var implementation0 = new Implementation("Text 0");
        var implementation1 = new Implementation("Text 1");
        var services = new ServiceContainer();
        services.AddService(typeof(IDependency), implementation0);
        var fixture = IoC
            .CreateFixture<TestTarget>()
            .Use(services);
        fixture.Arg(implementation1);

        var result = fixture.Resolve();

        result.Dep.ShouldBeSameAs(implementation1);
    }

    public interface IDependency;
}

file class TestTarget(ServiceProviderTests.IDependency dep)
{
    public ServiceProviderTests.IDependency Dep { get; } = dep;
}

file record Implementation(string Text) : ServiceProviderTests.IDependency;