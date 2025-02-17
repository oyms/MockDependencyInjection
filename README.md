Mock dependency injection tool
===

The purpose of this tool is to create test targets in unit tests, 
in projects where constructor injections of dependencies are used.

It tries to remove the need for using `new` directly, letting the
tool create the neccessary mocks needed to create an instance of the test target.


## TODO
- [ ] Create the base classes and contracts
- [ ] A basic Moq implementation
  - [ ] Define parameters/arguments
    - [ ] For interfaces
      - [ ] Defining mock behaviour (as an overload) 
    - [ ] For concrete instances
    - [ ] For nearest fit to a parameter 
- [ ] A NSubstitute implementation
- [ ] Include a service provider/DI container
- [ ] Verify that all setups are a part of the constructor
- [ ] Select the best matching constructor