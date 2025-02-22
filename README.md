Mock dependency injection tool
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

It seeks to remove the need for using `new` directly, letting the
tool create the neccessary mocks needed to create an instance of the test target.


## TODO
- [x] Create the base classes and contracts
- [x] A basic Moq implementation
  - [x] Define parameters/arguments
    - [x] For interfaces
      - [x] Defining mock behaviour (as an overload) 
    - [x] For concrete instances
    - [x] For nearest fit to a parameter
    - [x] For classes with constructors (recursive)
- [x] A NSubstitute implementation
- [x] A FakeItEasy implementation
- [x] Include a service provider/DI container
- [x] Verify that all setups are a part of the constructor
- [x] Select the best matching constructor
- [x] Solution for typed logger
- [ ] Documentation
  - [x] Code
  - [ ] Readme
- [ ] Packaging
  - [x] Icon
  - [ ] Publish script