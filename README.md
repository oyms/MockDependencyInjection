Mock dependency injection tool
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

It tries to remove the need for using `new` directly, letting the
tool create the neccessary mocks needed to create an instance of the test target.


## TODO
- [x] Create the base classes and contracts
- [x] A basic Moq implementation
  - [x] Define parameters/arguments
    - [x] For interfaces
      - [x] Defining mock behaviour (as an overload) 
    - [x] For concrete instances
    - [x] For nearest fit to a parameter
    - [ ] For classes with constructors (recursive)
    - [ ] Protected setup
- [ ] A NSubstitute implementation
- [ ] A RhinoMock implementation
- [ ] A FakeIt implementation
- [x] Include a service provider/DI container
- [x] Verify that all setups are a part of the constructor
- [ ] Select the best matching constructor
- [ ] Documentation
  - [ ] Code
  - [ ] Readme
- [ ] Packaging
  - [ ] Icon
  - [ ] Publish script