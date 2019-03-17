using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5 };
            var second = new[] { 5, 3, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void union_numbers_with_same_in_samegroup()
        {
            var first = new[] { 1, 3, 5, 5, 1 };
            var second = new[] { 5, 3, 7, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();

            var secondEnumerator = second.GetEnumerator();


            HashSet<int> hashResult = new HashSet<int>();

            while (firstEnumerator.MoveNext())
            {
                if (hashResult.Add(firstEnumerator.Current))
                {
                    yield return firstEnumerator.Current;
                }
            }

            while (secondEnumerator.MoveNext())
            {
                if (hashResult.Add(secondEnumerator.Current))
                {
                    yield return secondEnumerator.Current;
                }
            }

        }
    }
}