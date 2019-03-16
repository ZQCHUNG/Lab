using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyZipTests
    {
        [Test]
        public void pair_girls_and_keys()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girl, key) => $"{girl.Name}-{key.Owner}");

            var expected = new[]
            {
                "Mary-Joey",
                "Jessica-David",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        [Test]
        public void pair_girls_and_keys_carType()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girl, key) => $"{girl.Name}-{key.Type}");

            var expected = new[]
            {
                "Mary-BMW",
                "Jessica-TOYOTA",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        private IEnumerable<TResult> JoeyZip<T1, T2, TResult>(IEnumerable<T1> First, IEnumerable<T2> Second, Func<T1, T2, TResult> Selector)
        {
            var firstEnumerator = First.GetEnumerator();

            var secondEnumerator = Second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return Selector(firstEnumerator.Current, secondEnumerator.Current);
            }
        }
    }
}