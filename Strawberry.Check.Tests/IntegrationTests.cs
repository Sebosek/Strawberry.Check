using System;
using Strawberry.Check.Extensions;
using Xunit;

namespace Strawberry.Check.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void Verify_Objects_Success()
        {
var john = new Person
{
    Name = "John",
    Pet = new Pet {Name = "Jumpy"}
};

Check.Object(john, nameof(john))
    .NotNullOrWhiteSpaces(o => o.Name)
    .InnerObject(o => o.Pet, pet => pet
        .NotNullOrWhiteSpaces(o => o.Name))
    .Verify();
        }
        
        [Fact]
        public void Verify_InnerObjectIsNull_ArgumentNullException()
        {
            var john = new Person
            {
                Name = "John",
                Pet = null
            };

            Assert.Throws<ArgumentNullException>(() =>
                Check.Object(john, nameof(john))
                    .NotNullOrWhiteSpaces(person => person.Name)
                    .InnerObject(person => person.Pet, pet =>
                        pet.NotNullOrWhiteSpaces(p => p.Name))
                    .Verify());
        }
        
        [Fact]
        public void Sample()
        {
            var john = new Person
            {
                Name = "John",
                Pet = new Pet
                {
                    Name = "Jumpy"
                },
                BestFriend = new Person
                {
                    Name = "Jack",
                    Pet = new Pet
                    {
                        Name="Foxy"
                    }
                }
            };

            Check.Object(john, nameof(john))
                .NotNullOrWhiteSpaces(o => o.Name)
                .InnerObject(o => o.Pet, pet => pet
                    .NotNullOrWhiteSpaces(o => o.Name))
                .InnerObject(o => o.BestFriend, friend => friend
                    .NotNullOrWhiteSpaces(o => o.Name)
                    .InnerObject(o => o.Pet, pet => pet
                        .NotNullOrWhiteSpaces(o => o.Name)))
                .Verify();
        }

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
    }
}