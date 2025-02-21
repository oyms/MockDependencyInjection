using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Skaar.MockDependencyInjection.Resolving;

class MicrosoftLoggerResolver(Action<string> sink) : ServiceContainer
{
    public override bool TryResolve(Type type, [NotNullWhen(true)] out object? instance)
    {
        if (type == typeof(ILogger))
        {
            instance = new FakeLogger(sink);
            return true;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ILogger<>))
        {
            instance = Activator.CreateInstance(typeof(FakeLogger<>).MakeGenericType(type.GetGenericArguments()), [sink])!;
            return true;
        }

        instance = null;
        return false;
    }
}