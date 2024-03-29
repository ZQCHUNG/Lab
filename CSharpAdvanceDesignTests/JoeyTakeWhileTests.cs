﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyTakeWhileTests
    {
        [Test]
        public void take_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
            };

            //var actual = cards.TakeWhile(o => o.Kind == CardKind.Separate);//JoeyTakeWhile(cards);
            var actual = JoeyTakeWhile(cards);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards)
        {
            //var cardItems = new List<Card> { };

            foreach (var o in cards)
            {
                if (o.Kind == CardKind.Separate)
                {
                    yield break;;
                }
                else
                {
                    yield return o;
                }

                //cardItems.Add(o);
            }

            //return cardItems;
        }
    }
}