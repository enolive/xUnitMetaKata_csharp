using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class RoundTest
    {
        public RoundTest()
        {
            round = new Round();
        }

        private readonly Round round;

        [Fact]
        public void InvalidInputShouldMakeRoundFail()
        {
        }

        [Fact]
        public void PaperShouldWrapRock()
        {
            round.Play("Paper", "Rock").Should().Be(1);
            round.Play("Rock", "Paper").Should().Be(2);
        }

        [Fact]
        public void RockShouldBluntScissors()
        {
            round.Play("Rock", "Scissors").Should().Be(1);
            round.Play("Scissors", "Rock").Should().Be(2);
        }

        [Fact]
        public void SameChoiceShouldResultInDraw()
        {
            round.Play("Rock", "Rock").Should().Be(0);
            round.Play("Paper", "Paper").Should().Be(0);
            round.Play("Scissors", "Scissors").Should().Be(0);
        }

        [Fact]
        public void ScissorsShouldCutPaper()
        {
            round.Play("Scissors", "Paper").Should().Be(1);
            round.Play("Paper", "Scissors").Should().Be(2);
        }
    }
}