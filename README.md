
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

Operations : 
Registering Types (Reflection Components)
Default Registrarions 
Choice of constructor when registering the component
Registering Instances (Register Pre-defined components by me)
Lambda Expression Components
Open Generic Components


-- How to push to multiple repositories ? 
R : https://jigarius.com/blog/multiple-git-remote-repositories