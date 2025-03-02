Mock dependency injection tool using NSubstitute
===

The purpose of this tool is to create test targets in unit tests,
in projects where constructor injections of dependencies are used.

This package is used with [`MockDependencyInjection.Base`](https://www.nuget.org/packages/MockDependencyInjection.Base).

## Usage

This is used to utilize [NSubstitute](https://nsubstitute.github.io/) to
create mock objects of constructor arguments that are set up.

```C#
using Skaar.MockDependencyInjection.NSubstitute;

var fixture = IoC.CreateFixture<TestTarget>();
var mockOfDependency = fixture.Arg<IDependency>(); // Type used in constructor of TestTarget
var target = fixture.Resolve();
```

---

[Documentation on GitHub](https://github.com/oyms/MockDependencyInjection/blob/main/README.md)