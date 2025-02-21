namespace Skaar.MockDependencyInjection.Contracts;

/// <summary>
/// Is able to resolve to an instance
/// </summary>
public interface IResolvable
{
    /// <summary>
    /// Resolves the instance
    /// </summary>
    /// <returns>An instance of the type to resolve</returns>
    object Resolve();
}