namespace Skaar.MockDependencyInjection.Moq;

public static class IoC
{
    /// <summary>
    /// Creates a new Fixture. Use this to specify the arguments to send to the constructor of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The test target</typeparam>
    /// <returns>A new fixture</returns>
    public static Fixture<T> CreateFixture<T>() where T : class => new Fixture<T>();
    
    /// <summary>
    /// Creates a new <see cref="ServiceProvider"/> that can be used to resolve dependencies.
    /// </summary>
    public static ServiceProvider CreateServiceProvider() => new ServiceProvider();
}