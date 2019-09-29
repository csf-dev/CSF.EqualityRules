# Equality rules
A small library which provides a **fluent interface** to build & create instances of `IEqualityComparer<T>`, which test **value equality** between two object instances.

## Example
This example builds `comparer` which is an implementation of `IEqualityComparer<ASampleClass>`.  It will consider two objects equal if:
* Their `PropertyTwo` property values are equal
* *And* their `PropertyOne` property values are equal (using a case-insensitive match)
* `PropertyThree` *won't be considered* for determining equality

```csharp
// Here's a sample class to compare
public class ASampleClass
{
  public string PropertyOne { get; set; }
  public int PropertyTwo { get; set; }
  public DateTime PropertyThree { get; set; }
}

var comparer = new EqualityBuilder<ASampleClass>()
  .ForProperty(x => x.PropertyOne, c => c.UsingComparer(StringComparer.InvariantCultureIgnoreCase))
  .ForProperty(x => x.PropertyTwo)
  .Build();
```

## Integrating with Dependency Injection
The constructor for `EqualityBuilder` accepts an optional parameter of `IResolvesServices`.  The equality builder can use DI to resolve custom equality comparers (when they are chosen by type and not instance).  To do this, create an implementation of `IResolvesServices` in your own code (which should be a wrapper around your DI container's resolver) and provide it to the builder's constructor.
