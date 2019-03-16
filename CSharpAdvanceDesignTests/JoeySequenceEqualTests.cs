using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    [Ignore("not yet")]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal_1()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_2()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_3()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_4()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "456"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "789"},
            };


            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "456"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "789"},
            };


            //IEqualityComparer<Employee> equalityComparer = new JoeyEmployeeWithPhoneEqualityComparer();

            var actual = JoeySequenceEqual(
                first,
                second,
                new JoeyEmployeeWithPhoneEqualityComparer());

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return JoeySequenceEqual(first, second, EqualityComparer<TFirst>.Default);
        }


        private bool JoeySequenceEqual<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEqualityComparer<TFirst> ECompare)
        {
            bool res = true;

            var firstEnumerator = first.GetEnumerator();

            var secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext())
            {
                if (!secondEnumerator.MoveNext())
                {
                    return false;
                }

                var firstCurrent = firstEnumerator.Current;
                var secondCurrent = secondEnumerator.Current;
                

                //if (!ECompare.Equals(firstCurrent, secondCurrent))
                //{
                //    return false;
                //}
            }

            if (secondEnumerator.MoveNext())
            {
                return false;
            }

            return res;
        }
    }
}