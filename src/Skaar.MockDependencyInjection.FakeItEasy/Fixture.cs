using FakeItEasy.Creation;
using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Resolving;
using System.Reflection;

namespace Skaar.MockDependencyInjection.FakeItEasy;

/// <inheritdoc cref="Fixture{T,TFixture}"/>
/// <summary>
/// A fixture, responsible for resolving all constructor dependencies
/// to <see cref="global::FakeItEasy"/> fakes.
/// </summary>
/// <typeparam name="T">The test target</typeparam>
public class Fixture<T> : Fixture<T, Fixture<T>> where T : class
{
    /// <inheritdoc cref="Fixture{T,TFixture}"/>
    /// <summary>
    /// Sets up an argument to a fake
    /// </summary>
    /// <typeparam name="TA">The type of the argument value.</typeparam>
    /// <returns>A fake of <typeparamref name="TA"/>.</returns>
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
    
    /// <summary>
    /// Creates a new <see cref="Fixture{T,TFixture}"/> to provide a value to a constructor parameter.
    /// </summary>
    /// <param name="parameterName">
    /// (Optional) If provided, this will specify the parameter to apply this value to.
    /// </param>
    /// <typeparam name="TA">The type of the argument.</typeparam>
    /// <returns>A new fixture to specify the constructor arguments of that instance.</returns>
    /// <remarks>
    /// Use this when the constructor dependency is a class with dependencies (rather than an interface
    /// </remarks>
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
        return new FakeItEasyArgumentResolver(new ResolverSpecification(parameter));
    }
}