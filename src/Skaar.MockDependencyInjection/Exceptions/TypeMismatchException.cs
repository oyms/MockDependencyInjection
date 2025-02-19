namespace Skaar.MockDependencyInjection.Exceptions;

public class TypeMismatchException(Type type0, Type type1) : IoCException(
    $"There is a type mismatch between {type0.Name} and {type1.Name}")
{
    public Type SetupType { get; } = type0;
    public Type TargetType { get; } = type1;
}