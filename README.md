# GCFreeClosure
 A gc-free closure implementation for unity
 
 GC-Alloc has always been a kind of important performance measurement standard, so we should keep gc-alloc as less as possible.
 GCFreeClosure it designed for avoiding gc memory allocation.
 
 example:
 
```C#
//-----------------------------------------------------------------------------------
var action = ActionClosure.Create( ( a, b ) => UDebug.Print( a + b ), 1, 2 );
action.Invoke();

var function = FuncClosure.Create( ( a, b ) => a + b, 1, 2 );
var c = function.Invoke<int>();

// use Tuple to pack multi-value together
var function2 = FuncClosure.Create( ctx => ctx.Item1 + ctx.Item2, STuple.Create( 1, 2 ) );
// call with return value
var c2 = function2.Invoke<int>();

//-----------------------------------------------------------------------------------
var id = 1;
// system library implementation will cause gc-alloc for each function call.
list.FindIndex( e => e == id );

// replace system implementation to avoid gc-alloc
list.FindIndex( ( _id, e ) => e == _id, id );
```
 
