using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class GameTest
    {
        [Fact]
        public void Player1ShouldWin()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Scissors");
            game.PlayRound("Rock", "Scissors");

            var result = listener.Winner;
            result.Should().Be(1);
        }

        private class SpyGameListener : IGameListener
        {
            public void GameOver(int winner)
            {
                Winner = winner;
            }

            public int Winner { get; private set; }
        }
    }
}