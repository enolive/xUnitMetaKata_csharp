using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class Tests
    {
        [Fact]
        public void RockShouldBluntScissors()
        {
            new Round().Play("Rock", "Scissors").Should().Be(1);
            new Round().Play("Scissors", "Rock").Should().Be(2);
        }

        [Fact]
        public void ScissorsShouldCutPaper()
        {
            new Round().Play("Scissors", "Paper").Should().Be(1);
            new Round().Play("Paper", "Scissors").Should().Be(2);
        }

        [Fact]
        public void PaperShouldWrapRock()
        {
            new Round().Play("Paper", "Rock").Should().Be(1);
            new Round().Play("Rock", "Paper").Should().Be(2);
        }
    }
}