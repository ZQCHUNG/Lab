using System;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyContainsTests
    {
        [Test]
        public void contains_joey_chen()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey", LastName = "Wang"},
                new Employee(){FirstName = "Tom", LastName = "Li"},
                new Employee(){FirstName = "Joey", LastName = "Chen"},
            };

            var joey = new Employee() { FirstName = "Joey", LastName = "Chen" };

            var actual = JoeyContains(employees, joey, employesCurrent => employesCurrent.FirstName == joey.FirstName && employesCurrent.LastName == joey.LastName);

            Assert.IsTrue(actual);
        }

        private bool JoeyContains(IEnumerable<Employee> employees, Employee value, Func<Employee, bool> Predicate)
        {
            var employes = employees.GetEnumerator();

            while (employes.MoveNext())
            {
                var employesCurrent = employes.Current;
                
                if (Predicate(employesCurrent))
                {
                    return true;
                }
            }

            return false;

        }
    }
}