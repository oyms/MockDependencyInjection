Mock dependency injection tool
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

It seeks to remove the need for using `new` directly, letting the
tool create the neccessary mocks needed to create an instance of the test target.

> [!TIP]
> This works best when mostly interfaces are used to represent dependencies.

![Mock depencendy injection tool](./resources/logo.png)

## Usage

### Installation
[Add the nuget dependency to your test project](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package):

| *Mocking library*                             | *Nuget reference*                   | namespace                                          |
|-----------------------------------------------|-------------------------------------|----------------------------------------------------|
| [Moq](https://github.com/devlooped/moq)       | `skaar.mockdependency.moq`          | `using Skaar.MockDependencyInjection.Moq;`         |
| [NSubstitute](https://nsubstitute.github.io/) | `skaar.mockdependency.nsubstituteq` | `using Skaar.MockDependencyInjection.NSubstitute;` |
| [FakeItEasy](https://fakeiteasy.github.io/)   | `skaar.mockdependency.fakeiteasy`   | `using Skaar.MockDependencyInjection.FakeItEasy;`  |

### Use fixture

The _fixture_ is used to set up all constructor arguments,
and create an instance of the type, by calling the constructor with
the defined arguments.

```C#
// Create mock
var fixture = IoC.CreateFixture<ClassUnderTest>();
// Specify mock used in test
var mock = fixture.Arg<ITypeOfDependency>();
// Specify instances to be used as arguments
var dependency = fixture.Arg(new TypeOfDependency());
// Resolve the fixture to an instance
var testTarget = fixture.Resolve();
```

## Motivation

Given these types:

```C#
public class ClassContainingLogic(
    IDependency0 dependency0, 
    IDependency1 dependency1, 
    IDependency2 dependency2)
{
    public override string ToString()
    {
        return dependency1.Calculate(dependency0.GetValue()).ToString();
    }
}

public interface IDependency0
{
    int GetValue();
}

public interface IDependency1
{
    int Calculate(int value);
}

public interface IDependency2;
```

To test the logic of `ToString()` using Moq as mocking library,
the test may look like this:

```C#
using Moq;

public class TestClass
{
    private readonly Mock<IDependency0> _dependency0Mock;
    private readonly Mock<IDependency1> _dependency1Mock;
    private readonly ClassContainingLogic _target;

    // Test setup
    public TestClass()
    {
        // Set up dependencies of the test target
        _dependency0Mock = new Mock<IDependency0>();
        _dependency1Mock = new Mock<IDependency1>();
        var dependencyNotUsedInTest = new Mock<IDependency2>();
        
        // Create the test target
        _target = new ClassContainingLogic(
            _dependency0Mock.Object, 
            _dependency1Mock.Object, 
            dependencyNotUsedInTest.Object);
    }

    [Fact]
    public void ToString_ReturnsResultOfCalculation()
    {
        _dependency0Mock.Setup(x => x.GetValue()).Returns(10);
        _dependency1Mock.Setup(x => x.Calculate(10)).Returns(42);
        
        var result = _target.ToString();
        
        Assert.Equal("42", result);
    }
}
```
All the dependencies must be defined and set up in the test setup (the constructor).
The cost of maintaining such tests is evident when more dependencies
are introduced to `ClassContainingLogic`:
```C#
public class ClassContainingLogic(
    IDependency0 dependency0, 
    IDependency1 dependency1, 
    IDependency2 dependency2,
    IDependency3 dependency3)
{
    public override string ToString()
    {
        return dependency1.Calculate(dependency0.GetValue()).ToString();
    }
}
```
That will cause the test (and all tests like it) to fail to compile,
even though the new dependency is irrelevant for the test and the logic
it tests.

To avoid this nuisance, this library can be used to create the
test target:
```C#
// Test setup
public TestClass()
{
    var fixture = IoC.CreateFixture<ClassContainingLogic>();
    _dependency0Mock = fixture.Arg<IDependency0>();
    _dependency1Mock = fixture.Arg<IDependency1>();
    _target = fixture.Resolve();
}
```
The dependencies not used in the tests does not need to be specified.
Mocks will be created to satisfy the constructor's requirements.
The test will not break, when new dependencies are added to
`ClassContainingLogic`.

## Documentation

### Fixture

In the test setup method, create a fixture of the class under test 
(*TestTarget*).

`var fixture = IoC.CreateFixture<TestTarget>();`

The type of the `fixture` depends on the package and namespace of `IoC`
used.

#### Specifying a constructor argument as mock

The method `Arg<T>()` returns a mock of the type `T`. There must be a
constructor parameter in `TestTarget` that `T` is resolvable to.

Parameters that aren't specified, will be attempted created later.
It must be resolvable by the mocking library chosen.
For Moq, for instance, it must be an interface type, or a class
with a default (nullary) constructor.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/TargetWithInterfaceDependenciesTests.cs)

##### Optional parameters

- `parameterName`: `string` To explicitly specify which parameter this
  argument is to be applied to, this argument can be provided.
- `behavior`: `MockBehavior` (_Moq only_) The [behavior](https://documentation.help/Moq/90760C57.htm)
  of the created mock. 
  Defaults to `MockBehavior.Loose`.
- `callBase`: `bool` (_Moq only_) The [behavior](https://documentation.help/Moq/7B2F237D.htm)
  of the created mock. Defaults to `true`.
- `defaultValueProvider`: `DefaultValueProvider` (_Moq only_) The [behavior](https://documentation.help/Moq/635FA943.htm)
  of the created mock. Defaults to `DefaultValueProvider.Mock`.

Using the Moq package, a [`Mock<T>`](https://github.com/devlooped/moq/blob/main/src/Moq/Mock.cs)
is returned.
Using the other packages, a mock of type `T` is returned.
For instance, for the NSubstitute package, `Substitute.For<T>()` is called.

#### Specifying a constructor argument as instance

When you need to specify an argument as a specific instance,
use the other overload of `Arg<T>(T instance)`.

There must be a
constructor parameter in `TestTarget` that `T` is resolvable to.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/InstanceSetupTests.cs)

##### Optional parameters

- `parameterName`: `string` To explicitly specify which parameter this
  argument is to be applied to, this argument can be provided.

#### Specifying a constructor argument as a new fixture

When one of tthe constructor arguments is a concrete type (not an interface)
with a required constructor, you may specify it as a new fixture.

Use the method `ArgAsFixture<T>()`.
The method returns a new fixture, that may have constructor parameters set up.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/ArgumentAsFixtureTests.cs)

##### Optional parameters

- `parameterName`: `string` To explicitly specify which parameter this
  argument is to be applied to, this argument can be provided.

#### Include a service container

To use a service provider/DI container, the method `Use(serviceContainer)`
be used. The type [`ServiceContainer`](src/Skaar.MockDependencyInjection/Resolving/ServiceContainer.cs)
may be inherited to implement a DI container.
Perhaps more convenient, is the extension method 
`Skaar.MockDependencyInjection.Extensions.FixtureExtensions.Use()` that can take a standard 
`System.IServiceProvider` and wrap it in a container.

The container will be used to resolve constructor arguments after the
setups are exhausted.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/ServiceProviderTests.cs)

#### Resolve

To resolve the fixture to an instance of the `TestTarget`.
Use the method `Resolve()` to get an instance of the test target.

The fixture will attempt to find the constructor that matches the setup best.

An exception will be thrown if some of the setups (mocks) are unused.
This will probably indicate an error in the test setup.

Multiple calls to this method will return the same instance.

> [!WARNING]
> No further setups may be performed after this point.
> New calls to `Arg<T>` will fail, unless they have been setup
> previously.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/TargetWithoutDependenciesTests.cs)

#### Use log sink

When depending on `Microsoft.Extensions.Logging.ILogger<out TCategoryName>` and `TCategoryName`
is a non-public type, this may cause trouble with mocking libraries using 
the [Castle Proxy](https://www.castleproject.org/projects/dynamicproxy/).

You may use the extension method `Skaar.MockDependencyInjection.Extensions.FixtureExtensions.UseLogSink<T, TFixture>()`.
This will inject a `Microsoft.Extensions.Logging.Testing.FakeLogger`.
As argument, a log sink may be provided (of type ` Action<string>`).
It defaults to `System.Console.Out.WriteLine`.

For XUnit tests, use `ITestOutputHelper.WriteLine`.
For NUnit tests, use `TestContext.Write`.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/LogSinkTests.cs)

#### Use AutoFixture to create dependencies

[AutoFixture](https://autofixture.github.io/) is a library that can create instances of concrete types and populate
properties with values (recursively).

This may be used to fill in the dependencies not specified using the `Arg<T>` method.

Add a dependency to `skaar.mockdependency.autofixture` in the test project.

Create a new `AutoFixtureProvider` and add it to the fixture.

```C#
var provider = new Skaar.MockDependencyInjection.AutoFixture.AutoFixtureProvider();
fixture.Use(provider);
```
The [AutoFixture fixture](https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/Fixture.cs)
is exposed in the `Fixture` property, when specific setup is required.

[Example](tests/Skaar.MockDependencyInjection.Moq.Tests/Fixture/AutoFixtureProviderTests.cs)