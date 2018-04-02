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

            // player 1 wins game
            var listener = new SpyGameListener();
            var game = new Game(listener);
            game.PlayRound("Rock", "Scissors");
            game.PlayRound("Rock", "Scissors");

            var result = listener.Winner;
            if (result == 1)
            {
                runState.TestsPassed++;
                Console.WriteLine("player 1 wins game: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("player 1 wins game: FAIL - expected 1 but was {0}", result);
            }

            // player 2 wins game
            listener = new SpyGameListener();
            game = new Game(listener);
            game.PlayRound("Rock", "Paper");
            game.PlayRound("Rock", "Paper");

            result = listener.Winner;
            if (result == 2)
            {
                runState.TestsPassed++;
                Console.WriteLine("player 2 wins game: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("player 2 wins game: FAIL - expected 2 but was {0}", result);
            }

            // drawers not counted
            listener = new SpyGameListener();
            game = new Game(listener);
            game.PlayRound("Rock", "Rock");
            game.PlayRound("Rock", "Rock");

            result = listener.Winner;
            if (result == 0)
            {
                runState.TestsPassed++;
                Console.WriteLine("drawers not counted: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("drawers not counted: FAIL - expected 0 but was {0}", result);
            }

            //invalid moves not counted
            listener = new SpyGameListener();
            game = new Game(listener);
            try
            {
                game.PlayRound("Blah", "Foo");
                game.PlayRound("Rock", "Scissors");
            }
            catch (InvalidMoveException)
            {
            }

            result = listener.Winner;
            if (result == 0)
            {
                runState.TestsPassed++;
                Console.WriteLine("invalid moves not counted: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("invalid moves not counted: FAIL - expected 0 but was {0}", result);
            }
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

            // invalid inputs not allowed
            Exception exception = null;

            Action act = () => new Round().Play("Blah", "Foo");
            try
            {
                act();
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception is InvalidMoveException)
            {
                runState.TestsPassed++;
                Console.WriteLine("invalid inputs not allowed: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("invalid inputs not allowed: FAIL - expected InvalidMoveException");
            }
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

    internal class SpyGameListener : IGameListener
    {
        public int Winner { get; private set; }

        public void GameOver(int winner)
        {
            Winner = winner;
        }
    }
}