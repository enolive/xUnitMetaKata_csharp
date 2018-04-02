using System;

namespace RockPaperScissors.Test
{
    public class TestRunner
    {
        private readonly RunState runState = new RunState();
        private readonly Expectations expectations;

        public TestRunner()
        {
            expectations = new Expectations(runState);
        }

        public void Run()
        {
            // output header
            Console.WriteLine("Running RockPaperScissors tests...");

            RunRoundTests();
            RunGameTests();

            Console.WriteLine("Tests run: {0}  Passed: {1}  Failed: {2}",
                runState.Total,
                runState.TestsPassed,
                runState.TestsFailed);
        }

        private void RunGameTests()
        {
            // Game tests
            Console.WriteLine("Game tests...");

            RunTest(Player1WinsGame, "player 1 wins game");
            Player2WinsGame();
            DrawersNotCounted();
            InvalidMovesNotCounted();
        }

        private void RunTest(Action testMethod, string testName)
        {
            try
            {
                testMethod();
                runState.TestsPassed++;
                Console.WriteLine($"{testName}: PASS");
            }
            catch (ExpectationFailedException e)
            {
                runState.TestsFailed++;
                Console.WriteLine($"{testName}: FAIL - {e.Message}");
            }
        }

        private void InvalidMovesNotCounted()
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

            expectations.Expect(listener.Winner).ToBe(0, "invalid moves not counted");
        }

        private void DrawersNotCounted()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Rock");
            game.PlayRound("Rock", "Rock");

            expectations.Expect(listener.Winner).ToBe(0, "drawers not counted");
        }

        private void Player2WinsGame()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Paper");
            game.PlayRound("Rock", "Paper");

            expectations.Expect(listener.Winner).ToBe(2, "player 2 wins game");
        }

        private void Player1WinsGame()
        {
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Scissors");
            game.PlayRound("Rock", "Scissors");

            expectations.Expect(listener.Winner).ToBe(1);
        }

        private void RunRoundTests()
        {
            // Round tests
            Console.WriteLine("Round tests...");

            WinsAgainst("Rock", "Scissors", "rock blunts scissors");
            WinsAgainst("Scissors", "Paper", "scissors cut paper");
            WinsAgainst("Paper", "Rock", "paper wraps rock");

            RoundIsDraw("Rock");
            RoundIsDraw("Scissors");
            RoundIsDraw("Paper");

            InvalidInputsNotAllowed();
        }

        private void InvalidInputsNotAllowed()
        {
            expectations.Expect(() => new Round().Play("Blah", "Foo"))
                .ToThrow<InvalidMoveException>("invalid inputs not allowed");
        }

        private void RoundIsDraw(string player)
        {
            expectations.Expect(new Round().Play(player, player)).ToBe(0, $"round is a draw ({player}, {player})");
        }

        private void WinsAgainst(string player1, string player2, string testCase)
        {
            expectations.Expect(new Round().Play(player1, player2)).ToBe(1, $"{testCase} ({player1}, {player2})");
            expectations.Expect(new Round().Play(player2, player1)).ToBe(2, $"{testCase} ({player2}, {player1})");
        }
    }

    internal class ExpectationFailedException : Exception
    {
        public ExpectationFailedException(string message) : base(message)
        {
        }
    }

    internal class SpyGameListener : IGameListener
    {
        public int Winner { get; private set; }

        public void GameOver(int winner)
        {
            Winner = winner;
        }
    }
}