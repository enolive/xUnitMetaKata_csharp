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

            RunTest(GameTest.Player1WinsGame, "player 1 wins game");
            RunTest(GameTest.Player2WinsGame, "player 2 wins game");
            RunTest(GameTest.DrawersNotCounted, "drawers not counted");
            RunTest(GameTest.InvalidMovesNotCounted, "invalid moves not counted");
        }

        private void RunRoundTests()
        {
            // Round tests
            Console.WriteLine("Round tests...");

            RunTest(RoundTest.Winning("Rock", "Scissors"), "rock blunts scissors (Rock, Scissors)");
            RunTest(RoundTest.Losing("Rock", "Scissors"), "rock blunts scissors (Scissors, Rock)");
            RunTest(RoundTest.Winning("Scissors", "Paper"), "scissors cut paper (Scissors, Paper)");
            RunTest(RoundTest.Losing("Scissors", "Paper"), "scissors cut paper (Paper, Scissors)");
            RunTest(RoundTest.Winning("Paper", "Rock"), "paper wraps rock (Paper, Rock)");
            RunTest(RoundTest.Losing("Paper", "Rock"), "paper wraps rock (Rock, Paper)");

            RunTest(RoundTest.RoundIsDraw("Rock"), "round is a draw (Rock, Rock)");
            RunTest(RoundTest.RoundIsDraw("Scissors"), "round is a draw (Scissors, Scissors)");
            RunTest(RoundTest.RoundIsDraw("Paper"), "round is a draw (Paper, Paper)");

            RunTest(RoundTest.InvalidInputsNotAllowed, "invalid inputs not allowed");
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
    }

    internal class ExpectationFailedException : Exception
    {
        public ExpectationFailedException(string message) : base(message)
        {
        }
    }
}