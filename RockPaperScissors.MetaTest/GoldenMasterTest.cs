using System;
using System.IO;
using FluentAssertions;
using RockPaperScissors.Test;
using Xunit;

namespace RockPaperScissors.MetaTest
{
    public class GoldenMasterTest
    {
        private const string ExpectedOutput = @"Running RockPaperScissors tests...
Round tests...
rock blunts scissors (Rock, Scissors): PASS
rock blunts scissors (Scissors, Rock): PASS
scissors cut paper (Scissors, Paper): PASS
scissors cut paper (Paper, Scissors): PASS
paper wraps rock (Paper, Rock): PASS
paper wraps rock (Rock, Paper): PASS
round is a draw (Rock, Rock): PASS
round is a draw (Scissors, Scissors): PASS
round is a draw (Paper, Paper): PASS
invalid inputs not allowed: PASS
Game tests...
player 1 wins game: PASS
player 2 wins game: PASS
drawers not counted: PASS
invalid moves not counted: PASS
Tests run: 14  Passed: 14  Failed: 0
";

        [Fact]
        public void TestRunShouldHaveExpectedOutput()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                TestRunner.Main();
                OutputFrom(writer).Should().Be(ExpectedOutput);
            }
        }

        private static string OutputFrom(StringWriter writer) => writer.GetStringBuilder().ToString();
    }
}