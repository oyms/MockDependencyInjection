Mock dependency injection tool (base library)
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

This package is used by other packages.

| *Mocking library*                             | *Nuget reference*                                                                                           | namespace                                          |
|-----------------------------------------------|-------------------------------------------------------------------------------------------------------------|----------------------------------------------------|
| [Moq](https://github.com/devlooped/moq)       | [`MockDependencyInjection.Moq`](https://www.nuget.org/packages/MockDependencyInjection.Moq)                 | `using Skaar.MockDependencyInjection.Moq;`         |
| [NSubstitute](https://nsubstitute.github.io/) | [`MockDependencyInjection.NSubstitute`](https://www.nuget.org/packages/MockDependencyInjection.NSubstitute) | `using Skaar.MockDependencyInjection.NSubstitute;` |
| [FakeItEasy](https://fakeiteasy.github.io/)   | [`MockDependencyInjection.FakeItEasy`](https://www.nuget.org/packages/MockDependencyInjection.FakeItEasy)                                                                    | `using Skaar.MockDependencyInjection.FakeItEasy;`  |

---

[Documentation on GitHub](https://github.com/oyms/MockDependencyInjection/blob/main/README.md)