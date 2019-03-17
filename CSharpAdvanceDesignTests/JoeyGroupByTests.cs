using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Lee"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Lee"},
            };

            var actual = JoeyGroupBy(employees, 
                employee => employee.LastName);
            Assert.AreEqual(2, actual.Count());
            var firstGroup = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees, Func<Employee, string> KeySelectror)
        {
            var employeeEnumerator = employees.GetEnumerator();

            var dictionary = new Dictionary<string, List<Employee>>();

            while (employeeEnumerator.MoveNext())
            {
                var employee = employeeEnumerator.Current;

                if (dictionary.ContainsKey(KeySelectror(employee)))
                {
                    dictionary[employee.LastName].Add(employee);
                }
                else
                {
                    dictionary.Add(employee.LastName,new List<Employee>(){employee});
                }
            }

            return ConvertMultiGrouping(dictionary);
        }

        private IEnumerable<IGrouping<string, Employee>> ConvertMultiGrouping(Dictionary<string, List<Employee>> dictionary)
        {
            var dictEnumerator = dictionary.GetEnumerator();
            
            while (dictEnumerator.MoveNext())
            {
                var current = dictEnumerator.Current;

                yield return new Grouping(current.Key, current.Value);
            }

        }
    }

    internal class Grouping : IGrouping<string, Employee>
    {
        private string key;
        private List<Employee> value;

        public Grouping(string key, List<Employee> value)
        {
            this.key = key;
            this.value = value;
        }

        public string Key { get; }

        public IEnumerator<Employee> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}