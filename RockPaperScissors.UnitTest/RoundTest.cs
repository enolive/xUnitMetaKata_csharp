using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class RoundTest
    {
        public RoundTest()
        {
            target = new Round();
        }

        private readonly Round target;

        [Fact]
        public void InvalidInputShouldMakeRoundFail()
        {
            target.Invoking(x => x.Play("Blah", "Paper")).Should().Throw<InvalidMoveException>();
            target.Invoking(x => x.Play("Rock", "Foo")).Should().Throw<InvalidMoveException>();
        }

        [Fact]
        public void PaperShouldWrapRock()
        {
            ((int) target.Play("Paper", "Rock")).Should().Be(1);
            ((int) target.Play("Rock", "Paper")).Should().Be(2);
        }

        [Fact]
        public void RockShouldBluntScissors()
        {
            ((int) target.Play("Rock", "Scissors")).Should().Be(1);
            ((int) target.Play("Scissors", "Rock")).Should().Be(2);
        }

        [Fact]
        public void SameChoiceShouldResultInDraw()
        {
            ((int) target.Play("Rock", "Rock")).Should().Be(0);
            ((int) target.Play("Paper", "Paper")).Should().Be(0);
            ((int) target.Play("Scissors", "Scissors")).Should().Be(0);
        }

        [Fact]
        public void ScissorsShouldCutPaper()
        {
            ((int) target.Play("Scissors", "Paper")).Should().Be(1);
            ((int) target.Play("Paper", "Scissors")).Should().Be(2);
        }
    }
}