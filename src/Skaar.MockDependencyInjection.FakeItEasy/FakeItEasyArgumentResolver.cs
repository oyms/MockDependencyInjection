using FakeItEasy;
using Skaar.MockDependencyInjection.Contracts;
using System.Reflection;

namespace Skaar.MockDependencyInjection.FakeItEasy;

class FakeItEasyArgumentResolver(ResolverSpecification key, object? optionsBuilder = null) : IArgumentResolver
{
    private object? _fake;
    public object Resolve()
    {
        return _fake ??= CreateFake();
    }

    public ResolverSpecification Key { get; } = key;

    private object CreateFake()
    {
        if (optionsBuilder == null)
        {
            var method = typeof(A).GetMethod("Fake", [])!;
            var genericMethod = method.MakeGenericMethod(Key.ArgumentType);
            return genericMethod.Invoke(null, [])!;
        }
        else
        {
            var method = typeof(A)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == "Fake" && m.GetParameters().Length == 1);
            var genericMethod = method.MakeGenericMethod(Key.ArgumentType);
            return genericMethod.Invoke(null, [optionsBuilder])!;
        }
    }
}