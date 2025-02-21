using Skaar.MockDependencyInjection.Contracts;
using System.Reflection;

namespace Skaar.MockDependencyInjection.NSubstitute;

public class Fixture<T> : Skaar.MockDependencyInjection.Fixture<T, Fixture<T>> where T : class
{
    public TA Arg<TA>(string? parameterName = null)
    {
        var key = ResolverSpecification.New<TA>(parameterName);
        if (Resolvers[key] is not NSubstituteArgumentResolver resolver)
        {
            AssertNotResolved();
            resolver = new NSubstituteArgumentResolver(key);
            Resolvers.Add(resolver);
        }

        return (TA) resolver.Resolve();
    }

    protected override IArgumentResolver CreateArgumentResolver(ParameterInfo parameter)
    {
        return new NSubstituteArgumentResolver(new ResolverSpecification(parameter));
    }
}