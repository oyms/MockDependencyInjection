namespace Skaar.MockDependencyInjection.Moq;

public static class IoC
{
    public static Fixture<T> CreateFixture<T>() where T : class => new Fixture<T>();
}