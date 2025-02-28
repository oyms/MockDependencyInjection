Mock dependency injection tool using AutoFixture
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

This package is used with `MockDependencyInjection.Base`.

## Usage 

This is used to utilize [AutoFixture](https://autofixture.github.io/) to 
create objects of parameter types that are not interfaces and not otherwise setup.

```C#
var provider = new Skaar.MockDependencyInjection.AutoFixture.AutoFixtureProvider();
fixture.Use(provider);
```
The [AutoFixture fixture](https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/Fixture.cs)
is exposed in the `Fixture` property, when specific setup is required.

---

[Documentation on GitHub](https://github.com/oyms/MockDependencyInjection/blob/main/README.md)