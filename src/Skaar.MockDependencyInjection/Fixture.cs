using Skaar.MockDependencyInjection.Exceptions;
using System.Reflection;

namespace Skaar.MockDependencyInjection
{
    public abstract class Fixture<T> where T : class
    {
        private T? _resolved;
        private Type _targetType = typeof(T);

        protected virtual T CreateInstance()
        {
            AssessFeasibility();
            //Find constructor
            var constructors = _targetType.GetConstructors(BindingFlags.Public|BindingFlags.Instance);
            var constructor = constructors.First();
            //Require arguments

            //Call constructor
            return (T) constructor.Invoke(null);
        }

        public T Resolve() => _resolved = CreateInstance();

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
    }
}