
-- Make use of my AUTOFAC/DI Injection, IOC, knowledge to better describe the problem we had in millennium about 
the registration of Handlers was Singleton, and the impossibility of this objs to be put to garbage by Garbage Collector. 
And of course the linear raise of memory usage because of this objs.

-- For example, in Millennium Nuget, make easy to use Extension Method to register a bunch of depdencies using Reflection, with Instance per Lifetime Scope
	
- For example, in Millennium Nuget, make easy to use Extension Method to use Aggregate Service