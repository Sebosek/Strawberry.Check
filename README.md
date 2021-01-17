# Strawberry check

Guard clauses implementation for .NET.

Currently, the library contain a few the most common simple checks.

 - `Check.NotNull(o, nameof(o))`
   Throws ArgumentNullException
 - `Check.NotNullOrEmpty(o, nameof(o))`
   Thrown ArgumentNullException when `o` is null, otherwise ArgumentException if `o` is empty
 - `Check.NotNullOrWhiteSpace(o, nameof(o))`
   Thrown ArgumentNullException when `o` is null, otherwise ArgumentException if `o` contains only white spaces

But, let's take a little bit complicated example.

```csharp
private class Person
{
    public string Name { get; init; }
    public Pet Pet { get; init; }
    public Person BestFriend { get; set; }
}

private class Pet
{
    public string Name { get; init; }
}
```

How can we guard that the name of person and his/her pet's name are filled in?

```csharp
var john = new Person
{
    Name = "John",
    Pet = new Pet {Name = "Max"}
};

Check.Object(john, nameof(john))
    .NotNullOrWhiteSpaces(o => o.Name)
    .InnerObject(o => o.Pet, pet => pet
        .NotNullOrWhiteSpaces(o => o.Name))
    .Verify();
```

