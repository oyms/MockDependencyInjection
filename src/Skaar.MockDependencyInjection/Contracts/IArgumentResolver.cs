namespace Skaar.MockDependencyInjection.Contracts
{
    public interface IArgumentResolver
    {
        ResolverSpecification Key { get; }
        object Resolve();
    }
}