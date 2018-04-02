using System;

namespace RockPaperScissors.Test
{
    public class TestRunner
    {
        private readonly RunState runState = new RunState();

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

            RockBluntsScissors();

            var result = new Round().Play("Scissors", "Rock");
            if (result == 2)
            {
                runState.TestsPassed++;
                Console.WriteLine("rock blunts scissors (Scissors, Rock): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("rock blunts scissors (Scissors, Rock): FAIL - expected 2 but was {0}", result);
            }

            // scissors cut paper
            result = new Round().Play("Scissors", "Paper");
            if (result == 1)
            {
                runState.TestsPassed++;
                Console.WriteLine("scissors cut paper (Scissors, Paper): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("scissors cut paper (Scissors, Paper): FAIL - expected 1 but was {0}", result);
            }

            result = new Round().Play("Paper", "Scissors");
            if (result == 2)
            {
                runState.TestsPassed++;
                Console.WriteLine("scissors cut paper (Paper, Scissors): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("scissors cut paper (Paper, Scissors): FAIL - expected 2 but was {0}", result);
            }

            // paper wraps rock
            result = new Round().Play("Paper", "Rock");
            if (result == 1)
            {
                runState.TestsPassed++;
                Console.WriteLine("paper wraps rock (Paper, Rock): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("paper wraps rock (Paper, Rock): FAIL - expected 1 but was {0}", result);
            }

            result = new Round().Play("Rock", "Paper");
            if (result == 2)
            {
                runState.TestsPassed++;
                Console.WriteLine("paper wraps rock (Rock, Paper): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("paper wraps rock (Rock, Paper): FAIL - expected 2 but was {0}", result);
            }

            // round is a draw
            result = new Round().Play("Rock", "Rock");
            if (result == 0)
            {
                runState.TestsPassed++;
                Console.WriteLine("round is a draw (Rock, Rock): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("round is a draw (Rock, Rock): FAIL - expected 0 but was {0}", result);
            }

            result = new Round().Play("Scissors", "Scissors");
            if (result == 0)
            {
                runState.TestsPassed++;
                Console.WriteLine("round is a draw (Scissors, Scissors): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("round is a draw (Scissors, Scissors): FAIL - expected 0 but was {0}", result);
            }

            result = new Round().Play("Paper", "Paper");
            if (result == 0)
            {
                runState.TestsPassed++;
                Console.WriteLine("round is a draw (Paper, Paper): PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine("round is a draw (Paper, Paper): FAIL - expected 0 but was {0}", result);
            }

            // invalid inputs not allowed
            Exception exception = null;

            try
            {
                new Round().Play("Blah", "Foo");
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

        private void RockBluntsScissors()
        {
            var result = new Round().Play("Rock", "Scissors");
            AssertEquals(result, 1, "rock blunts scissors (Rock, Scissors)");
        }

        private void AssertEquals(int result, int expected, string message)
        {
            if (result == expected)
            {
                runState.TestsPassed++;
                Console.WriteLine($"{message}: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine($"{message}: FAIL - expected {expected} but was {result}");
            }
        }

        private class RunState
        {
            public int TestsPassed { get; set; }
            public int TestsFailed { get; set; }
            public int Total => TestsPassed + TestsFailed;
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