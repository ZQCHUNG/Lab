﻿using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = JoeDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employees()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyDistinctWithEqualityComparer(employees, new EmployeeEqualityComparer());

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyDistinctWithEqualityComparer<TSource>(IEnumerable<TSource> Sources, IEqualityComparer<TSource> equalityComparer)
        {
            //return new HashSet<int>(numbers);

            var hashSet = new HashSet<TSource>(equalityComparer);

            var source = Sources.GetEnumerator();

            while (source.MoveNext())
            {
                var current = source.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        private IEnumerable<TSource> JoeDistinct<TSource>(IEnumerable<TSource> Sources)
        {
            return JoeyDistinctWithEqualityComparer(Sources, EqualityComparer<TSource>.Default);
        }
        
    }
}