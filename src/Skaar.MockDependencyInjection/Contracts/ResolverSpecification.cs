using System.Reflection;

namespace Skaar.MockDependencyInjection.Contracts
{
    public record ResolverSpecification(Type ArgumentType, string? ParameterName)
    {
        public ResolverSpecification(ParameterInfo parameterInfo) : this(parameterInfo.ParameterType,
            parameterInfo.Name){}
        public static ResolverSpecification New<T>(string? name = null) => new(typeof(T), name);
        public override string ToString()
        {
            if(ParameterName is null) return ArgumentType.FullName ?? ArgumentType.Name;
            return $"Parameter: {ParameterName} {ArgumentType.Name}";
        }
    }
}