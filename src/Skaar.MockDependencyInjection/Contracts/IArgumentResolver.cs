namespace Skaar.MockDependencyInjection.Contracts;

public interface IArgumentResolver: IResolvable
{
    ResolverSpecification Key { get; }
}