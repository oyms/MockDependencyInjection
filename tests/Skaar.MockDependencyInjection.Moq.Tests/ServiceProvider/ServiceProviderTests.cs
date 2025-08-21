using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Skaar.MockDependencyInjection.AutoFixture;

namespace Skaar.MockDependencyInjection.Moq.Tests.ServiceProvider;

public class ServiceProviderTests
{
    private readonly Moq.ServiceProvider _target;

    public ServiceProviderTests()
    {
        _target = new  Skaar.MockDependencyInjection.Moq.ServiceProvider();  
    }
    
    [Fact]
    public void AddService_ShouldAddMockToServiceProvider()
    {
        _target.AddService<ServiceProviderTests_TestType1>();
        
        var sp = _target as IServiceProvider;

        sp.GetRequiredService<ServiceProviderTests_TestType1>().ShouldNotBeNull();
        sp.GetRequiredService<ServiceProviderTests_TestType2>().ShouldNotBeNull();
    }   
    
    [Fact]
    public void Use_ServiceProvder_ShouldUseProvider()
    {
        _target.AddService<ServiceProviderTests_TestType1>();
        _target.Use(new AutoFixtureProvider());
        
        var sp = _target as IServiceProvider;

        sp.GetRequiredService<ServiceProviderTests_TestType1>().ShouldNotBeNull();
        sp.GetRequiredService<ServiceProviderTests_TestType3>().Text.ShouldNotBeNull();
    }
}

public interface ServiceProviderTests_TestType1;
public interface ServiceProviderTests_TestType2;

public class ServiceProviderTests_TestType3
{
    public string Text { get; set; }
}