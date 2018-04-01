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
            target.Invoking(x => x.Play((PlayChoice)42, PlayChoice.Paper)).Should().Throw<InvalidMoveException>();
            target.Invoking(x => x.Play(PlayChoice.Rock, (PlayChoice)42)).Should().Throw<InvalidMoveException>();
        }

        [Fact]
        public void PaperShouldWrapRock()
        {
            target.Play(PlayChoice.Paper, PlayChoice.Rock).Should().Be(1);
            target.Play(PlayChoice.Rock, PlayChoice.Paper).Should().Be(2);
        }

        [Fact]
        public void RockShouldBluntScissors()
        {
            target.Play(PlayChoice.Rock, PlayChoice.Scissors).Should().Be(1);
            target.Play(PlayChoice.Scissors, PlayChoice.Rock).Should().Be(2);
        }

        [Fact]
        public void SameChoiceShouldResultInDraw()
        {
            target.Play(PlayChoice.Rock, PlayChoice.Rock).Should().Be(0);
            target.Play(PlayChoice.Paper, PlayChoice.Paper).Should().Be(0);
            target.Play(PlayChoice.Scissors, PlayChoice.Scissors).Should().Be(0);
        }

        [Fact]
        public void ScissorsShouldCutPaper()
        {
            target.Play(PlayChoice.Scissors, PlayChoice.Paper).Should().Be(1);
            target.Play(PlayChoice.Paper, PlayChoice.Scissors).Should().Be(2);
        }
    }
}