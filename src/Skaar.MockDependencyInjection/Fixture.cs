using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Exceptions;
using System.Reflection;

namespace Skaar.MockDependencyInjection
{
    public abstract class Fixture<T> where T : class
    {
        private T? _resolved;
        private readonly Type _targetType = typeof(T);

        protected virtual T CreateInstance()
        {
            AssessFeasibility();
            //Find constructor
            var constructors = _targetType.GetConstructors(BindingFlags.Public|BindingFlags.Instance);
            var constructor = SelectConstructor(constructors);
            //Require arguments
            var args = constructor.GetParameters().Select(GetArgumentInstance);
            //Call constructor
            return (T) constructor.Invoke(args.ToArray())!;
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
                    $"The type {_targetType.Name} is is a generic type definition and cannot be created.");
            }
        }
        
        protected void AssertNotResolved()
        {
            if (_resolved is not null)
            {
                throw new FixtureIsResolvedException<T>(_resolved);
            }
        }

        private object GetArgumentInstance(ParameterInfo parameter)
        {
            var key = new ResolverSpecification(parameter);
            if(Resolvers.TryResolve(key, out var resolver))
            {
                return resolver.Resolve();
            }
            resolver = CreateArgumentResolver(parameter);
            Resolvers.Add(resolver);
            return resolver.Resolve();
        }
    }
}