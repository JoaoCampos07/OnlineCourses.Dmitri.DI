
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