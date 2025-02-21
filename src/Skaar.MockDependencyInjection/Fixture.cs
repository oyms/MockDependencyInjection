using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Exceptions;
using Skaar.MockDependencyInjection.Resolving;
using System.Reflection;
using ServiceContainer = Skaar.MockDependencyInjection.Resolving.ServiceContainer;

namespace Skaar.MockDependencyInjection;

/// <summary>
/// The base class responsible for resolving arguments for the parameters of the constructor of <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T">The test target.</typeparam>
/// <typeparam name="TFixture">The concrete fixture (creating the actual mocks using a mock library)</typeparam>
/// <remarks>
/// The correct constructor will be selected based upon the setups done.
/// If possible, the default (nullary) constructor will be avoided.
/// </remarks>
public abstract class Fixture<T, TFixture>: IResolvable where T : class where TFixture: Fixture<T, TFixture>
{
    private T? _resolved;
    private readonly Type _targetType = typeof(T);
    private readonly ServiceContainerCollection _serviceContainers = new();

    /// <summary>
    /// Sets up an argument with an instance value.
    /// </summary>
    /// <param name="instance">The argument value to send to the constructor.</param>
    /// <param name="parameterName">
    /// (Optional) If provided, this will specify the parameter to apply this value to.
    /// </param>
    /// <typeparam name="TI">The type of the argument value.</typeparam>
    /// <returns>The value of <paramref name="instance"/></returns>
    public TI Arg<TI>(TI instance, string? parameterName = null) where TI : notnull
    {
        AssertNotResolved();
        var resolver = InstanceArgumentResolver.From(instance, parameterName);
        Resolvers.Add(resolver);
        return instance;
    }

    /// <summary>
    /// Use this method to provide a Ioc-container/service provider for parameters that is not set up.
    /// </summary>
    /// <param name="serviceContainer">The service container to inject, providing values.</param>
    /// <returns>This fixture</returns>
    public TFixture Use(ServiceContainer serviceContainer)
    {
        _serviceContainers.Add(serviceContainer);
        return (TFixture) this;
    }

    /// <summary>
    /// When the target has dependency on <see cref="ILogger"/>
    /// or <see cref="ILogger{TCategoryName}"/>, this method can be used to inject
    /// a fake logger <seealso cref="FakeLogger"/> to direct the log output.
    /// </summary>
    /// <param name="sink">
    /// The output to write the log messages to.
    /// Defaults to <see cref="Console"/>.
    /// </param>
    /// <returns>This fixture</returns>
    /// <remarks>
    /// When using xUnit, use <c>ITestOutputHelper.WriteLine</c> as <paramref name="sink"/>,
    /// to get the log output in the test results.
    /// </remarks>
    public TFixture UseLogSink(Action<string>? sink = null)
    {
        _serviceContainers.Add(new MicrosoftLoggerResolver(sink ?? Console.Out.WriteLine));
        return (TFixture) this;
    }

    /// <summary>
    /// Resolves the test target instance.
    /// </summary>
    /// <returns>
    /// An instance of <typeparamref name="T"/>
    /// with all constructor dependencies resolved.
    /// </returns>
    /// <exception cref="IoCException">
    /// Exceptions are thrown when setups are failing to resolve,
    /// or some of the setups remain unused.
    /// </exception>
    /// <remarks>
    /// No setups may be performed after the target is resolved.
    /// When called multiple times, the same instance is returned. 
    /// </remarks>
    public T Resolve() => _resolved ??= CreateInstance();

    protected virtual T CreateInstance()
    {
        AssessFeasibility();
            
        var constructors = _targetType.GetConstructors(BindingFlags.Public|BindingFlags.Instance);
        var constructor = SelectConstructor(constructors);

        var args = GetConstructorArguments(constructor);
            
        return (T) constructor.Invoke(args)!;
    }

    /// <inheritdoc cref="IResolvable"/>
    object IResolvable.Resolve() => Resolve();

    protected IArgumentResolverCollection Resolvers { get; } = new ArgumentResolverCollection();

    protected abstract IArgumentResolver  CreateArgumentResolver(ParameterInfo parameter);

    protected virtual ConstructorInfo SelectConstructor(IReadOnlyCollection<ConstructorInfo> constructors)
    {
        if(!constructors.Any()) throw new ArgumentException("No constructors found", nameof(constructors));
        if(constructors.Count == 1) return constructors.First();
        return constructors
            .Where(c => c.GetParameters().Any())
            .OrderByDescending(c => c
            .GetParameters()
            .Select(p => Resolvers.TryResolve(new ResolverSpecification(p), out var resolved ) ? resolved : null)
            .Count(r => r != null))
            .First();
    }

    protected virtual void AssessFeasibility()
    {
        if (_targetType.IsInterface)
        {
            throw new TypeCannotBeCreatedException(
                $"The type {_targetType.Name} is an interface type and cannot be created.");
        }
        if (_targetType.IsAbstract)
        {
            throw new TypeCannotBeCreatedException(
                $"The type {_targetType.Name} is abstract and cannot be created.");
        }
        if (_targetType.IsGenericTypeDefinition)
        {
            throw new TypeCannotBeCreatedException(
                $"The type {_targetType.Name} is a generic type definition and cannot be created.");
        }
    }

    protected void AssertNotResolved()
    {
        if (_resolved is not null)
        {
            throw new FixtureIsResolvedException<T>(_resolved);
        }
    }

    private object[] GetConstructorArguments(ConstructorInfo constructor)
    {
        var allSetups = Resolvers.Keys.ToList();
        var parameters = constructor.GetParameters();
        var resolvers = parameters.Select(GetOrCreateArgumentResolver).ToList();
        var notUsed = allSetups.Except(resolvers.Select(r => r.Key)).ToArray();
        if (notUsed.Any())
        {
            throw new UnusedSetupException(notUsed, constructor);
        }
        return resolvers.Select(r => r.Resolve()).ToArray();
    }

    private IArgumentResolver GetOrCreateArgumentResolver(ParameterInfo parameter)
    {
        var key = new ResolverSpecification(parameter);
        if(Resolvers.TryResolve(key, out var resolver))
        {
            return resolver;
        }
        if (_serviceContainers.TryResolve(key.ArgumentType, out var instance))
        {
            var instanceArgumentResolver = new InstanceArgumentResolver(new ResolverSpecification(parameter), instance);
            Resolvers.Add(instanceArgumentResolver);
            return instanceArgumentResolver;
        }
        resolver = CreateArgumentResolver(parameter);
        Resolvers.Add(resolver);
        return resolver;
    }
}