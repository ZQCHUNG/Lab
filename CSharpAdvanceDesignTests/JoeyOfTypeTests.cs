using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
                {"validator2", new ProductPriceValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            Assert.AreEqual(2, validators.Count());
        }

        [Test]
        public void get_special_type_value_from_arguments_vaild_false()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
                {"validator2", new ProductPriceValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            var product = JoeyOfType<Product>(arguments.Values).Single();

            var isValid = validators.All(x => x.Validate(product));
            
            Assert.IsFalse(isValid);
            //Assert.AreEqual(1, validators.Count());
        }

        private IEnumerable<T> JoeyOfType<T>(Dictionary<string, object>.ValueCollection values)
        {
            var value = values.GetEnumerator();
            
            while (value.MoveNext())
            {
                var current = value.Current;

                if (current is T item)
                {
                    yield return (T)item;
                }
                
            }

        }
    }
}