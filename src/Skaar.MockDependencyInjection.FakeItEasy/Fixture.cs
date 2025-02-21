using FakeItEasy.Creation;
using Skaar.MockDependencyInjection.Contracts;
using System.Reflection;

namespace Skaar.MockDependencyInjection.FakeItEasy;

public class Fixture<T> : Skaar.MockDependencyInjection.Fixture<T, Fixture<T>> where T : class
{
    public TA Arg<TA>(string? parameterName = null, Action<IFakeOptions<TA>>? optionsBuilder = null) where TA : class
    {
        var key = ResolverSpecification.New<TA>(parameterName);
        if (Resolvers[key] is not FakeItEasyArgumentResolver resolver)
        {
            AssertNotResolved();
            resolver = new FakeItEasyArgumentResolver(key, optionsBuilder);
            Resolvers.Add(resolver);
        }
        return (TA) resolver.Resolve();
    }
    protected override IArgumentResolver CreateArgumentResolver(ParameterInfo parameter)
    {
        return new FakeItEasyArgumentResolver(new ResolverSpecification(parameter), null);
    }
}