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
            game.PlayRound(PlayChoice.Rock, PlayChoice.Scissors);
            game.PlayRound(PlayChoice.Rock, PlayChoice.Scissors);
            listener.Winner.Should().Be(1);
        }

        [Fact]
        public void Player2ShouldWin()
        {
            game.PlayRound(PlayChoice.Rock, PlayChoice.Paper);
            game.PlayRound(PlayChoice.Rock, PlayChoice.Paper);
            listener.Winner.Should().Be(2);
        }

        [Fact]
        public void DrawersShouldNotBeCounted()
        {
            game.PlayRound(PlayChoice.Rock, PlayChoice.Rock);
            game.PlayRound(PlayChoice.Rock, PlayChoice.Rock);
            listener.Winner.Should().Be(0);
        }

        [Fact]
        public void InvalidChoiceShouldNotBeCounted()
        {
            game.PlayRound((PlayChoice)42, PlayChoice.Rock);
            game.PlayRound(PlayChoice.Rock, (PlayChoice)42);
            listener.Winner.Should().Be(0);
        }

        [Fact]
        public void OneRoundShouldNotResultAWinner()
        {
            game.PlayRound(PlayChoice.Rock, PlayChoice.Paper);
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