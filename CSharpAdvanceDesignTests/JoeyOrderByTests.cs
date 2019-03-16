using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> KeySelector, Comparer<TKey> KeyComparer)
        {
            this.KeySelector = KeySelector;
            this.KeyComparer = KeyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private Comparer<TKey> KeyComparer { get; set; }

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
        public void lastName_firstName_Age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboComparer(firstComparer, secondComparer);

            var thirdComparer = new CombineKeyComparer<int>(element => element.Age, Comparer<int>.Default);

            var finalCombo = new ComboComparer(firstCombo, thirdComparer);

            var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

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

            var firstkeyComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondkeyComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var actual = JoeyOrderBy(employees,
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

        private IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees, 
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