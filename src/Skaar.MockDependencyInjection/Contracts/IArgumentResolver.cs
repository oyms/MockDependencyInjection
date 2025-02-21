namespace Skaar.MockDependencyInjection.Contracts;

/// <summary>
/// Represents something that should be able to resolve a constructor parameter into an argument value.
/// </summary>
public interface IArgumentResolver: IResolvable
{
    /// <summary>
    /// The parameter specification
    /// </summary>
    ResolverSpecification Key { get; }
}