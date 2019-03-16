using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    // [Ignore("not yet")]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var actual = JoeyReverse(employees);

            var expected = new List<Employee>
            {
                new Employee(){FirstName = "David",LastName = "Wang"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "Joey",LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyReverse<TSource>(IEnumerable<TSource> Sources)
        {
            return new Stack<TSource>(Sources);

            //var employes = Sources.GetEnumerator();

            //Stack<TSource> esStack = new Stack<TSource>();

            //while (employes.MoveNext())
            //{
            //    esStack.Push(employes.Current);
            //}

            //while (esStack.Any())
            //{
            //    yield return esStack.Pop();
            //}
        }
    }
}