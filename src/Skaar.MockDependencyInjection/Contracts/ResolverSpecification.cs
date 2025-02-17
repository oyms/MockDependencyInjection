using System.Reflection;

namespace Skaar.MockDependencyInjection.Contracts
{
    public record ResolverSpecification(Type ArgumentType, string? ArgumentName)
    {
        public ResolverSpecification(ParameterInfo parameterInfo) : this(parameterInfo.ParameterType,
            parameterInfo.Name){}
        public static ResolverSpecification New<T>(string? name = null) => new(typeof(T), name);
    }
}