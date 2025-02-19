using Skaar.MockDependencyInjection.Contracts;
using System.Reflection;

namespace Skaar.MockDependencyInjection.Exceptions
{
    public class UnusedSetupException(ResolverSpecification[] setups, ConstructorInfo constructor)
        : IoCException(
            $"Argument setup not used in constructor: {string.Join(", ", setups.Select(s => s.ToString()))}")
    {
        public IEnumerable<ResolverSpecification> Setups { get; } = setups;
        public ConstructorInfo Constructor { get; } = constructor;
    }
}