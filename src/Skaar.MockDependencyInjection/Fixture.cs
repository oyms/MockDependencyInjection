using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Exceptions;
using Skaar.MockDependencyInjection.Resolving;
using System.ComponentModel.Design;
using System.Reflection;
using ServiceContainer = Skaar.MockDependencyInjection.Resolving.ServiceContainer;

namespace Skaar.MockDependencyInjection
{
    public abstract class Fixture<T, TFixture> where T : class where TFixture: Fixture<T, TFixture>
    {
        private T? _resolved;
        private readonly Type _targetType = typeof(T);
        private ServiceContainerCollection _serviceContainers = new();

        public TI Arg<TI>(TI instance, string? parameterName = null) where TI : notnull
        {
            AssertNotResolved();
            var resolver = InstanceArgumentResolver.From(instance, parameterName);
            Resolvers.Add(resolver);
            return instance;
        }

        public TFixture Use(ServiceContainer serviceContainer)
        {
            _serviceContainers.Add(serviceContainer);
            return (TFixture) this;
        }

        protected virtual T CreateInstance()
        {
            AssessFeasibility();
            
            var constructors = _targetType.GetConstructors(BindingFlags.Public|BindingFlags.Instance);
            var constructor = SelectConstructor(constructors);

            var args = GetConstructorArguments(constructor);
            
            return (T) constructor.Invoke(args)!;
        }

        public T Resolve() => _resolved ??= CreateInstance();

        protected IArgumentResolverCollection Resolvers { get; } = new ArgumentResolverCollection();

        protected abstract IArgumentResolver  CreateArgumentResolver(ParameterInfo parameter);

        protected virtual ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors.OrderByDescending(c => c.GetParameters().Length).First();
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
            var resolvers = parameters.Select(GetArgumentResolver).ToList();
            var notUsed = allSetups.Except(resolvers.Select(r => r.Key)).ToArray();
            if (notUsed.Any())
            {
                throw new UnusedSetupException(notUsed, constructor);
            }
            return resolvers.Select(r => r.Resolve()).ToArray();
        }

        private IArgumentResolver GetArgumentResolver(ParameterInfo parameter)
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
}