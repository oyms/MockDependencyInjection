using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Skaar.MockDependencyInjection.AutoFixture;

namespace Skaar.MockDependencyInjection.Moq.Tests.ServiceProvider;

public class ServiceProviderTests
{
    private readonly Moq.ServiceProvider _target;

    public ServiceProviderTests()
    {
        _target = IoC.CreateServiceProvider();
    }
    
    [Fact]
    public void AddService_ShouldAddMockToServiceProvider()
    {
        _target.AddService<TestType1>();
        
        var sp = _target as IServiceProvider;

        sp.GetRequiredService<TestType1>().ShouldNotBeNull();
        sp.GetRequiredService<TestType2>().ShouldNotBeNull();
    }   
    
    [Fact]
    public void Use_ServiceProvder_ShouldUseProvider()
    {
        _target.AddService<TestType1>();
        _target.Use(new AutoFixtureProvider());
        
        var sp = _target as IServiceProvider;

        sp.GetRequiredService<TestType1>().ShouldNotBeNull();
        sp.GetRequiredService<TestType3>().Text.ShouldNotBeNull();
    }

    [Fact]
    public void Use_ServiceProviderInFixture_IsUsedForResolving()
    {
        var serviceProvider = IoC.CreateServiceProvider();
        var mock1 = serviceProvider.AddService<TestType1>();
        var fixture0 = IoC.CreateFixture<TestType4>().Use(serviceProvider);
        var fixture1 = IoC.CreateFixture<TestType4>().Use(serviceProvider);
        
        var result0 = fixture0.Resolve();
        var result1 = fixture1.Resolve();

        result0.Type1.ShouldBeSameAs(mock1.Object);
        result1.Type1.ShouldBeSameAs(mock1.Object);
        result0.Type2.ShouldBeSameAs(result1.Type2);
        
    }
    public interface TestType1;
     public interface TestType2;
     
     public class TestType3
     {
         public string Text { get; set; }
     }

     public record TestType4(TestType1 Type1, TestType2 Type2);
}

