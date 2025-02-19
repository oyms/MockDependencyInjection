using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Contracts
{
    public interface IArgumentResolverCollection
    {
        IArgumentResolver? this[ResolverSpecification key]  { get;}
        bool TryResolve(ResolverSpecification key, [NotNullWhen(true)] out IArgumentResolver? resolver);
        void Add(IArgumentResolver resolver);
        IEnumerable<ResolverSpecification> Keys { get; }
    }
}