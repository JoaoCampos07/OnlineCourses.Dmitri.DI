
-- AUTOFAC -- 
-- Terminology -- 

Component : (the class itself like ConsoleLog)
	A body of code that declares the services it provides and the dependencies it consumes.

Services : (interfaces like ILog)
	A contract between a providing and consuming component

Dependency :
    A service required by a component (normally they appear in the constructor)

Container : 
    The dependecies are fullfil by a container. Manages the components that make up the application.

Section : Registration Concepts
1) Scenario (without DI)
2) Registering Types (Reflection Components)
3) Default Registrations
4) Choice of Constructor
5) Registering Instances (Instance Components)
6) Lambda Expression Components
7) Open Generic Components

Section : Advanced Registration Concepts
1) Passing Parameters to Register()
2) Delegate Factories
3) Objects on Demand
4) Property and Method Injection
5) Scanning for Types
6) Scanning for Modules

Section : Implicit Relationship Types
1) Delayed Instantion
2) Controlled Instantion
3) Dynamic Instantion 
4) Parameterized Instantion 
5) Enumeration 
6) Metadata Interrogation
7) Keyed Service Lookup
8) Container INdependence

-- How to push to multiple repositories ? 
R : https://jigarius.com/blog/multiple-git-remote-repositories