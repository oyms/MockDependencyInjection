using Moq;
using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Resolving;
using System.Reflection;

namespace Skaar.MockDependencyInjection.Moq;

public class Fixture<T> : Skaar.MockDependencyInjection.Fixture<T, Fixture<T>> where T : class
{
    public Mock<TA> Arg<TA>(
        string? parameterName = null, 
        MockBehavior behavior = MockBehavior.Loose,
        bool callBase = true,
        DefaultValueProvider? defaultValueProvider = null) where TA : class
    {
        var key = ResolverSpecification.New<TA>(parameterName);
        if (Resolvers[key] is not MockArgumentResolver resolver)
        {
            AssertNotResolved();
            var config = new MoqConfig(behavior, callBase, defaultValueProvider);
            resolver = new MockArgumentResolver(key, config);
            Resolvers.Add(resolver);
        }
        return (Mock<TA>)resolver.Mock;
    }

    public Fixture<TA> ArgAsFixture<TA>(string? parameterName = null) where TA : class
    {
        AssertNotResolved();
        var fixture = new Fixture<TA>();
        var resolver = new ResolvableResolver(ResolverSpecification.New<TA>(parameterName), fixture);
        Resolvers.Add(resolver);
        return fixture;
    }

    protected override IArgumentResolver CreateArgumentResolver(ParameterInfo parameter)
    {
        var key = new ResolverSpecification(parameter);
        return new MockArgumentResolver(key);
    }
}