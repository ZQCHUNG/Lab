using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer
    {
        public CombineKeyComparer(Func<Employee, string> firstKeySelector, Comparer<string> firstkeyComparer)
        {
            FirstKeySelector = firstKeySelector;
            FirstkeyComparer = firstkeyComparer;
        }

        public Func<Employee, string> FirstKeySelector { get; private set; }
        public Comparer<string> FirstkeyComparer { get; private set; }
    }

    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyOrderByTests
    {
        //[Ignore("q")]
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees, employee => employee.LastName, employee1 => employee1.FirstName);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };


            var firstkeyComparer = new CombineKeyComparer(element => element.LastName, Comparer<string>.Default);
            var secondkeyComparer = new CombineKeyComparer(element => element.FirstName, Comparer<string>.Default);

            var actual = JoeyOrderByLastNameAndFirstName(employees,
                firstkeyComparer,
                secondkeyComparer);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees, CombineKeyComparer FirstCombineKeyComparer,  CombineKeyComparer SecondCombineKeyComparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    var firstCompareResult = FirstCombineKeyComparer.FirstkeyComparer.Compare(FirstCombineKeyComparer.FirstKeySelector(element), FirstCombineKeyComparer.FirstKeySelector(minElement));
                    if (firstCompareResult < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        if (SecondCombineKeyComparer.FirstkeyComparer.Compare(SecondCombineKeyComparer.FirstKeySelector(element), SecondCombineKeyComparer.FirstKeySelector(minElement)) < 0)
                        {
                            minElement = element;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}