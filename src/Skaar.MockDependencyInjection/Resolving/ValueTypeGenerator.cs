using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving
{
    public class ValueTypeGenerator: ServiceContainer
    {
        public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
        {
            if (type.IsValueType)
            {
                instance = Activator.CreateInstance(type)!;
                return true;
            }

            if (type == typeof(string))
            {
                instance = Guid.NewGuid().ToString();
                return true;
            }

            instance = default;
            return false;
        }
    }
}