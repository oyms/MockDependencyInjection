using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Contracts;

/// <summary>
/// A collection of argument resolvers. <seealso cref="IArgumentResolver"/>
/// </summary>
public interface IArgumentResolverCollection
{
 
    /// <summary>
    /// This returns a resolver, if it has a <see cref="IArgumentResolver.Key"/> that matches
    /// <paramref name="key"/> exactly.
    /// </summary>
    /// <param name="key">A lookup key</param>
    IArgumentResolver? this[ResolverSpecification key]  { get;}
    /// <summary>
    /// Tries to resolve the <paramref name="key"/> to a resolver in the collection.
    /// </summary>
    /// <param name="key">A lookup key</param>
    /// <param name="resolver">The resolver found.</param>
    /// <returns><c>true</c> if a match is found.</returns>
    /// <remarks>
    /// This will first try an exact match (using <see cref="ResolverSpecification.ParameterName"/>
    /// if it is defined, before trying matching by  <see cref="ResolverSpecification.ArgumentType"/>.
    /// </remarks>
    bool TryResolve(ResolverSpecification key, [NotNullWhen(true)] out IArgumentResolver? resolver);
    /// <summary>
    /// Adds a resolver to the collection.
    /// </summary>
    /// <param name="resolver">The resolver to add.</param>
    void Add(IArgumentResolver resolver);
    /// <summary>
    /// Returns all the keys of the collection.
    /// </summary>
    IEnumerable<ResolverSpecification> Keys { get; }
}