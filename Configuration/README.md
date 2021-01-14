

-- QUESTIONS :

-- What is a module ? 
R: A module is a smal class that can bundle a set of related components behind a facade.
	Simplifies configuration and deployment. Exposes a restricted set of configuration parameters that can vary independentyly of components (we work with modules)
	Modules themselves do not go throught dependency injection.  Used to configure container, not registered or resolved.

-- Why we use it ? 
R: 1) Decreased configuration complexity
   2) Configuration parameters are explicit. Limits the configuration that is possible since his per Module, not per component
   3) Abstraction from internal application architecture - Modules hides the details of component configuration
   4) Better type safety
   5) Dynamic Configuration (we just need to change configuration file)
   6) Advanced Extensions : Modules can be used for more than simple type registration, attach component resolution events...

-- Use Cases 
R: 1) Configure related services that provide a subsystem (If we have a system, we can split in sub-systems by assembly level, and configure services in which of this. We can loaded latter. )
   2) Package application features as plug-ins -> We put a DLL in some directories and them scan for modules there and add them to the container.
   3) Provide pre-built packages for integration -> Pre-Package a assembly with some specific configuration and send it for other uses.  
   4) (Grouping) Register a number of similiar services that are often used together 
		- JSON/XML configuration implemented using a module.