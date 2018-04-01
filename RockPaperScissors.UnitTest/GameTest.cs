using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class GameTest
    {
        private readonly SpyGameListener listener;
        private readonly Game game;

        public GameTest()
        {
            listener = new SpyGameListener();
            game = new Game(listener);
        }

        [Fact]
        public void Player1ShouldWin()
        {
            game.PlayRound("Rock", "Scissors");
            game.PlayRound("Rock", "Scissors");
            listener.Winner.Should().Be(1);
        }

        [Fact]
        public void Player2ShouldWin()
        {
            game.PlayRound("Rock", "Paper");
            game.PlayRound("Rock", "Paper");
            listener.Winner.Should().Be(2);
        }

        [Fact]
        public void DrawersShouldNotBeCounted()
        {
            game.PlayRound("Rock", "Rock");
            game.PlayRound("Rock", "Rock");
            listener.Winner.Should().Be(0);
        }

        [Fact]
        public void OneRoundShouldNotResultAWinner()
        {
            game.PlayRound("Rock", "Paper");
            listener.Winner.Should().Be(0);
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