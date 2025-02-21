using System.Reflection;

namespace Skaar.MockDependencyInjection.Contracts;

/// <summary>
/// A type used to match <see cref="ParameterInfo"/> with <see cref="IArgumentResolver"/>.
/// </summary>
/// <param name="ArgumentType">The type of the argument (must be resolvable to the parameter type.</param>
/// <param name="ParameterName">An optional parameter name.</param>
public record ResolverSpecification(Type ArgumentType, string? ParameterName)
{
    /// <summary>
    /// Constructs a spec using both type and name of the parameter.
    /// </summary>
    /// <param name="parameterInfo">The parameter to specify.</param>
    public ResolverSpecification(ParameterInfo parameterInfo) : this(parameterInfo.ParameterType,
        parameterInfo.Name){}
    /// <summary>
    /// Constructs a spec using the type of an argument and (optionally) the name of a parameter.
    /// </summary>
    /// <param name="name">The parameter name</param>
    /// <typeparam name="T">The type of the argument</typeparam>
    /// <returns>A specification</returns>
    public static ResolverSpecification New<T>(string? name = null) => new(typeof(T), name);
    /// <summary>
    /// Represents the specification as a string
    /// </summary>
    public override string ToString()
    {
        if(ParameterName is null) return ArgumentType.FullName ?? ArgumentType.Name;
        return $"Parameter: {ParameterName} {ArgumentType.Name}";
    }
}