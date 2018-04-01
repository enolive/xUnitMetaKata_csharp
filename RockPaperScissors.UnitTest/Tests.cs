using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class Tests
    {
        [Fact]
        public void RockBluntsScissors()
        {
            new Round().Play("Rock", "Scissors").Should().Be(1);
            new Round().Play("Scissors", "Rock").Should().Be(2);
        }
    }
}