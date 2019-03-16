using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, string> KeySelector, Comparer<string> KeyComparer)
        {
            this.KeySelector = KeySelector;
            this.KeyComparer = KeyComparer;
        }

        private Func<Employee, string> KeySelector { get; set; }
        private Comparer<string> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> FirstCombineKeyComparer, IComparer<Employee> SecondCombineKeyComparer)
        {
            this.FirstCombineKeyComparer = FirstCombineKeyComparer;
            this.SecondCombineKeyComparer = SecondCombineKeyComparer;
        }

        public IComparer<Employee> FirstCombineKeyComparer { get; private set; }
        public IComparer<Employee> SecondCombineKeyComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var FirstCompare = FirstCombineKeyComparer.Compare(x, y);

            if (FirstCompare == 0)
            {
                return SecondCombineKeyComparer.Compare(x, y);
            }

            return FirstCompare;
        }
    }

    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyOrderByTests
    {
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
                new ComboComparer(firstkeyComparer, secondkeyComparer));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees, 
                    IComparer<Employee> comboComparer)
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

                    var firstCompareResult = comboComparer.Compare(element, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        var secondCompareResult = comboComparer.Compare(element, minElement);
                        if (secondCompareResult < 0)
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