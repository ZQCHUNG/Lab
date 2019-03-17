using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyConcatTests
    {
        [Test]
        public void concat_two_employees()
        {
            var first = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var second = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Li"},
                new Employee {FirstName = "Tom", LastName = "Wang"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyConcat(first, second);

            var expected = new List<Employee>()
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Li"},
                new Employee {FirstName = "Tom", LastName = "Wang"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyConcat(IEnumerable<Employee> firstSource, IEnumerable<Employee> secondSource)
        {
            //List<Employee> employees = new List<Employee>();
            
            var first = firstSource.GetEnumerator();

            var second = secondSource.GetEnumerator();

            while (first.MoveNext())
            {
                yield return first.Current;
            }

            while (second.MoveNext())
            {
                yield return second.Current;
            }

           // return employees;
        }
    }
}