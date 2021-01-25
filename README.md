
-- AUTOFAC -- 
-- Terminology -- 

Component : (the class itself like ConsoleLog)
	A body of code that declares the services it provides and the dependencies it consumes.

Services : (interfaces like ILog)
	A contract between a providing and consuming component

Example : builder.RegisterType<MyComponent>().As<IService>();

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

Section : Controlling Scope and Lifetime
 1) Instance Scope
 2) Captive Dependencies
 3) Disposal 
 4) Lifetime Events
 5) Running code at Startup

Section : Configuration
 1) Using Modules
 2) JSON/XML COnfiguration with Microsoft.Extensions.Configuration
 3) Component Options
 4) Configuration of Modules

Section : Configuration
 1) Registration Souces : Let you affect the process of container type resolution and add additional registrations unexplicit.
 2) Adapter Pattern using DI Container like Autofac
 3) Decorator Pattern using DI Container like Autofac
 4) Circular depdencies
 5) Attribute Based Metadata
 6) Aggregate Services
 7) Type Interceptors

-- How to push to multiple repositories ? 
R : https://jigarius.com/blog/multiple-git-remote-repositories