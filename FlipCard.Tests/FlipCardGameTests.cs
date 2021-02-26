using System;
using Xunit;
using System.Linq;

namespace FlipCards.Tests
{
    public class FlipCardGameTests
    {
        [Fact]
        public void Run()
        {
            var game = new FlipCardGame(4);

            while (!game.IsOver)
            {
                var coverdCards = game.Cards.Where(c => !c.Visable);
                var random = new Random();
                var index = random.Next(0, coverdCards.Count() - 1);
                var flipCard = coverdCards.ElementAt(index);

                game.Flip(flipCard.X, flipCard.Y);
            }

            Assert.True(game.Cards.All(c => c.Visable));
            Console.WriteLine(game.FlipCount);
        }
    }
}
