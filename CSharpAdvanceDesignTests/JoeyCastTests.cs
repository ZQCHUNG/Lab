using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyCastTests
    {
        [Test]
        public void cast_int_exception_when_ArrayList_has_string()
        {
            var arrayList = new ArrayList { 1, "2", 3 };

            void TestDelegate() => JoeyCast<int>(arrayList.ToString());

            Assert.Throws<JoeyCastException>(TestDelegate);
        }

        private IEnumerable<T> JoeyCast<T>(IEnumerable source)
        {
            var sourceEnumerator = source.GetEnumerator();

            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;

                if (current is T item)
                {
                    yield return item;
                }
                else
                {
                    throw new JoeyCastException();
                }
            }

        }
    }

    internal class JoeyCastException : Exception
    {

    }
}