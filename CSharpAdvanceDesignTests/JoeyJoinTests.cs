using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees,
                pets,
                (employee, pet) => Tuple.Create(employee.FirstName, pet.Name), pet1 => pet1, employee1 => employee1);

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void all_pets_name_first_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees,
                pets,
                (employee, pet) => $"{pet.Name}-{pet.Owner.FirstName}", pet1 => pet1, employee1 => employee1);

            var expected = new[]
            {
                "Didi-David",
                "Lala-Joey",
                "QQ-Joey",
                "Fufu-Tom",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TOuter,TInner,TResult>(
            IEnumerable<TOuter> employees, 
            IEnumerable<TInner> pets, 
            Func<TOuter, TInner, TResult> ResultSelector,
            Func<TInner, Pet> petKeySelector, 
            Func<TOuter, Employee> employeeKeySelector)
        {
            var employeeEnumerator = employees.GetEnumerator();

            var petEnumerator = pets.GetEnumerator();

            while (employeeEnumerator.MoveNext())
            {
                var employee = employeeEnumerator.Current;

                while (petEnumerator.MoveNext())
                {
                    var pet = petEnumerator.Current;

                    var keyEqualityComparer = EqualityComparer<string>.Default;

                    if (petKeySelector(pet).Owner.Equals(employeeKeySelector(employee)))
                    {
                        yield return ResultSelector(employee, pet);
                    }
                }

                petEnumerator.Reset();
            }
        }
    }
}