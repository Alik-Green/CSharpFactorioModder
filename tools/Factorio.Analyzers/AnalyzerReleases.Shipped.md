; Release tracking for the analyzer package. See docs/ANALYZERS.md for each rule.

## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
FX0001 | CSharpLua | Error | `using static` of a Factorio type crashes the pinned compiler
FX0002 | CSharpLua | Error | C# arrays need CoreSystem
FX0003 | CSharpLua | Error | `foreach` compiles to System.each
FX0004 | CSharpLua | Error | tuples need CoreSystem
FX0005 | CSharpLua | Warning | explicit casts are emitted as System.cast
FX0006 | CSharpLua | Error | integer types emit Lua 5.3 operators
FX0007 | CSharpLua | Error | string.Format / Convert / formatted interpolation
FX0008 | CSharpLua | Error | namespace needs the .NET runtime
FX0009 | CSharpLua | Error | try/throw/lock/using/async/yield/delegate/event
FX0010 | CSharpLua | Warning | template uses {0} as its receiver
FX0011 | CSharpLua | Warning | template contains a table literal with a quoted key
FX0012 | Factorio  | Warning | Storage.Data used as a receiver
FX0013 | CSharpLua | Error | `is`/`typeof` need runtime type information
FX0014 | Factorio  | Error | .NET type implemented only by CoreSystem
