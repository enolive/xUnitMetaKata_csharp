using System;
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
            target.Play("Paper", "Rock").Should().Be(1);
            target.Play("Rock", "Paper").Should().Be(2);
        }

        [Fact]
        public void RockShouldBluntScissors()
        {
            target.Play("Rock", "Scissors").Should().Be(1);
            target.Play("Scissors", "Rock").Should().Be(2);
        }

        [Fact]
        public void SameChoiceShouldResultInDraw()
        {
            target.Play("Rock", "Rock").Should().Be(0);
            target.Play("Paper", "Paper").Should().Be(0);
            target.Play("Scissors", "Scissors").Should().Be(0);
        }

        [Fact]
        public void ScissorsShouldCutPaper()
        {
            target.Play("Scissors", "Paper").Should().Be(1);
            target.Play("Paper", "Scissors").Should().Be(2);
        }
    }
}