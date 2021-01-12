
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