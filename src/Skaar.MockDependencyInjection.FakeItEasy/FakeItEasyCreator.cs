using FakeItEasy;
using FakeItEasy.Creation;
using System.Reflection;

namespace Skaar.MockDependencyInjection.FakeItEasy;

static class FakeItEasyCreator
{
    public static T CreateFake<T>(Action<IFakeOptions<T>>? optionsBuilder = null) where T : class => (T) CreateFake(typeof(T), optionsBuilder);

    public static object CreateFake(Type targetType, object? optionsBuilder = null)
    {
        if (optionsBuilder == null)
        {
            var method = typeof(A).GetMethod("Fake", [])!;
            var genericMethod = method.MakeGenericMethod(targetType);
            return genericMethod.Invoke(null, [])!;
        }
        else
        {
            var method = typeof(A)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == "Fake" && m.GetParameters().Length == 1);
            var genericMethod = method.MakeGenericMethod(targetType);
            return genericMethod.Invoke(null, [optionsBuilder])!;
        }
    }
}