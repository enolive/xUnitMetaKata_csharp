using FluentAssertions;
using Xunit;

namespace RockPaperScissors.UnitTest
{
    public class Tests
    {
        [Fact]
        public void RockBluntsScissors()
        {
            var result = new Round().Play("Rock", "Scissors");
            result.Should().Be(1);
        }
    }
}