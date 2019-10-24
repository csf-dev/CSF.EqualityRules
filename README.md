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

## Documentation
Full documentation is available on [the wiki for this repository].  This includes an exploration of how to add rules (including functionality not demonstrated in the example above).  It also includes a short piece on integrating with dependency injection frameworks.

[the wiki for this repository]: https://github.com/csf-dev/CSF.EqualityRules/wiki
