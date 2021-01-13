

Section : Implicit Relationship Types
Class 1) Delayed Instantion
Class 2) Controlled Instantion
Class 3) Dynamic Instantion 
Class 4) Parameterized Instantion 
Class 5) Enumeration 
Class 6) Metadata Interrogation
Class 7) Keyed Service Lookup
Class 8) Container INdependence

QUESTIONS :

What is a Implicit Relationshipt Type ? 
R:  A direct dependency relationshipt is when, a component needs another. For example, like a Car was a direct dependency to the motor.
	Car(Engine engine) { this.engine = engine;}

	Which means, was that car contructor is invoked is contructing param is a newly constructed engine.
	They are more or less built at the same time.

	In a implicit relatioship types, instead of just injecting the "engine" that the "car" needs. 
	I might inject a Func<Engine> or a Lazy<Engine>. This change the behavior of how the Injection Mecanism works !

Implicit Relationship Types : 

Lazy Dependency : Used when we have expensive to construct objects. So, the object is only loaded when I try to get the value. 
				  Container will Auto-initialize it with code(callback function/delegate) that resolves T, when we try to get his value.
				  Getting myField.Value constructs and returns a fully-initialized T (whatever that is...)

Owned Dependency : Can be released by the owner when no longer requeried. So this is cool to dispose objects safely and automatically for us.
				   So, is particular useful for IDisposable because Autofac handles the disposal.
				   Using myField.Value access the owned objec. 
				   use myField.Dispose() any time you want.

Dynamic Instantion : Injects an auto-generated feactory for my component
					 Allows me to Resolve<T>() without dependencies of AutoFAC. Without making use of their API's. (Of course, behind this scenes it uses the container) 
					 I dont use something like builder.Register((c, p) => new Reporting(c.Resolve<ConsoleLog>())).As<ILog>();
					 instead i inject in my component the dependency like so : Func<T>...
					 Them Why call myField() to construct the dependency with all his dependencies initialized.
					 If i decide to abandon AUTOFAC I still calling a FUNC<T> a type of .NET. I not using AUTOFAC.

Parameterized Instantiation : Like the previous one make an auto-generated factory.
							  with this one i can provide a Func<TArg1, TArg2, T> instead of just Func<T>
							  Them I call myField("John", 123)

Enumeration : Injectin an enumeration (e.g.: IEnumerable<T>, IList<T> or ICollection<T>...)
			  ... gets you one of each registered object of type T
			  Register both COnsoleLog and SMSLog for the service ILog, if I inject IList<ILog>. I will have an new of instance of ConsoleLog and SMSLog 
			  for me to use.
			  This is cool to safe resolve some objects.
				scope.Resolve<ILog>() will throw if no ILog services registered
				scope.Resolve<IList<ILog>>() will yield an empty list, NO exception !

Metadata Interrogation : I can attach metadata to components and make conditional logic based on this Metadata that is passed.
						 Is used injecting and storing a Meta<T>
						 RegisterType<T>().WithMetadata(...)
							- Weakly typed
							- Strongly typed (lambda syntax)
						 We can access this metadata like so : 
							myField.Metadata["foo"]
							myField.Value -> to get the object inside : T

Keyed Service Lookup : Simlar to the Enumeration but is like using a Dictionary with Key/Values
					   , so that we can pick up a service using the right key
					   we used injecting and storing IIndex<TKey,T>
					   Register with : 
						builder.RegisterType<ConsoleLog>().Keyed<ILog>("sms")