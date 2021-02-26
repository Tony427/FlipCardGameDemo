using System;
using System.Collections.Generic;
using System.Linq;

namespace FlipCards
{
    public class FlipCardGame
    {
        private readonly int _arraySize;
        private Card _previous;
        private Card _current;

        public bool IsOver => Cards.All(c => c.Visable);
        public int FlipCount = 0;
        public List<Card> Cards = new List<Card>();

        public FlipCardGame(int size)
        {
            if (size % 2 == 1)
                throw new ArgumentException("size number should be even.");

            _arraySize = size;

            NewGame();
        }

        private void NewGame()
        {
            // generate number set
            var numbers = Enumerable.Range(1, _arraySize * _arraySize / 2);
            var numberSet = numbers.Concat(numbers).ToArray();

            // shuffle numbers
            var random = new Random();
            for (int n = numberSet.Length - 1; n > 0; --n)
            {
                int k = random.Next(n + 1);
                int temp = numberSet[n];
                numberSet[n] = numberSet[k];
                numberSet[k] = temp;
            }

            // init Cards
            for (int x = 0; x < _arraySize; x++)
            {
                for (int y = 0; y < _arraySize; y++)
                {
                    Cards.Add(new Card { X = x, Y = y });
                }
            }

            // init values
            for (int i = 0; i < numberSet.Length; i++)
            {
                Cards[i].Value = numberSet[i];
            }
        }

        public void Flip(int x, int y)
        {
            // return if game over
            if (IsOver) return;

            _current = Cards.Single(c => c.Match(x, y));

            // can't flip card already visable
            if (_current.Visable) return;

            // add flip count
            FlipCount++;

            // filp card
            _current.Visable = true;

            // if the second card is not match. then cover these 2 cards.
            if (_previous?.Value != _current.Value && FlipCount % 2 == 0)
            {
                _current.Visable = false;
                _previous.Visable = false;
            }

            // assign to previous
            _previous = _current;
        }
    }

    public class Card
    {
        public Card()
        {
            Visable = false;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visable { get; set; }
        public int Value { get; set; }

        public bool Match(int x, int y)
            => X == x && Y == y;

        public bool Match(Card card)
            => Match(card.X, card.Y);
    }
}
