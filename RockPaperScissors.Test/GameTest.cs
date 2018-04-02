using static RockPaperScissors.Test.Expectations;

namespace RockPaperScissors.Test
{
    internal static class GameTest
    {
        public static void InvalidMovesNotCounted()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            try
            {
                game.PlayRound("Blah", "Foo");
                game.PlayRound("Rock", "Scissors");
            }
            catch (InvalidMoveException)
            {
            }

            Expect(listener.Winner).ToBe(0);
        }

        public static void DrawersNotCounted()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Rock");
            game.PlayRound("Rock", "Rock");

            Expect(listener.Winner).ToBe(0);
        }

        public static void Player2WinsGame()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Paper");
            game.PlayRound("Rock", "Paper");

            Expect(listener.Winner).ToBe(2);
        }

        public static void Player1WinsGame()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Scissors");
            game.PlayRound("Rock", "Scissors");

            Expect(listener.Winner).ToBe(1);
        }

        private class SpyGameListener : IGameListener
        {
            public int Winner { get; private set; }

            public void GameOver(int winner)
            {
                Winner = winner;
            }
        }
    }
}