using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Skaar.MockDependencyInjection.NSubstitute.Tests.ServiceProvider;

public class ServiceProviderTests
{
    private readonly NSubstitute.ServiceProvider _target;

    public ServiceProviderTests()
    {
        _target = IoC.CreateServiceProvider();
    }
    
    [Fact]
    public void AddService_ShouldAddMockToServiceProvider()
    {
        _target.AddService<ServiceProviderTests_TestType1>();
        
        var sp = _target as IServiceProvider;

        sp.GetRequiredService<ServiceProviderTests_TestType1>().ShouldNotBeNull();
        sp.GetRequiredService<ServiceProviderTests_TestType2>().ShouldNotBeNull();
    }   
}

public interface ServiceProviderTests_TestType1;
public interface ServiceProviderTests_TestType2;
