using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyExceptTests
    {
        [Test]
        public void except_numbers()
        {
            var first = new[] { 1, 3, 5, 7, 3 };
            var second = new[] { 7, 1, 4, 1 };

            var actual = JoeyExcept(first, second);
            var expected = new[] { 3, 5 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyExcept(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();

            HashSet<int> secondHash = new HashSet<int>(second);

            HashSet<int> Hash = new HashSet<int>();

            while (firstEnumerator.MoveNext())
            {
                if (secondHash.Add(firstEnumerator.Current))
                {
                    yield return firstEnumerator.Current;
                }
            }

        }
    }
}