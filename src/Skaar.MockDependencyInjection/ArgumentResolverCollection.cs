using Skaar.MockDependencyInjection.Contracts;
using Skaar.MockDependencyInjection.Exceptions;

namespace Skaar.MockDependencyInjection
{
    class ArgumentResolverCollection : IArgumentResolverCollection
    {
        private readonly Dictionary<ResolverSpecification, IArgumentResolver> _resolvers = new();

        public IArgumentResolver? this[ResolverSpecification key] => _resolvers.GetValueOrDefault(key);

        public bool TryResolve(ResolverSpecification key, out IArgumentResolver? resolver)
        {
            //Find exact match
            if(key.ArgumentName is not null)
            {
                resolver = GetResolverByName(key.ArgumentName);
                if (resolver is not null)
                {
                    if (!key.ArgumentType.IsAssignableFrom(resolver.Key.ArgumentType))
                    {
                        throw new TypeMismatchException(resolver.Key.ArgumentType, key.ArgumentType);
                    }
                    return true;
                }
            }

            resolver = GetResolverByType(key.ArgumentType).FirstOrDefault();
            return resolver is not null;
        }

        public void Add(IArgumentResolver resolver)
        {
            var name = resolver.Key.ArgumentName;
            if (name is not null && GetResolverByName(name) is not null)
            {
                throw new DuplicateParameterSetupException(name);
            }
            if (!_resolvers.TryAdd(resolver.Key, resolver))
            {
                throw new ParameterIsAlreadySetupException(resolver.Key);
            }
        }

        private IArgumentResolver? GetResolverByName(string name) =>
            _resolvers.Keys.Where(k => k.ArgumentName == name).ToArray().Length switch
            {
                0 => null,
                1 => _resolvers[_resolvers.Keys.Where(k => k.ArgumentName == name).ToArray()[0]],
                _ => throw new DuplicateParameterSetupException(name)
            };

        private IEnumerable<IArgumentResolver> GetResolverByType(Type type)
        {
            foreach (var key in _resolvers.Keys.Where(k => k.ArgumentType == type)
                         .Where(k => k.ArgumentName is null))
            {
                yield return _resolvers[key];
            }
            foreach (var key in _resolvers.Keys.Where(k => k.ArgumentType.IsAssignableFrom(type))
                         .Where(k => k.ArgumentName is null))
            {
                yield return _resolvers[key];
            }
        }
    } 
}