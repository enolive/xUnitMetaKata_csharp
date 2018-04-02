using System;
using static RockPaperScissors.Test.Expectations;

namespace RockPaperScissors.Test
{
    internal static class RoundTest
    {
        public static void InvalidInputsNotAllowed()
        {
            Expect(() => new Round().Play("Blah", "Foo"))
                .ToThrow<InvalidMoveException>();
        }

        public static Action RoundIsDraw(string player) => () =>
            Expect(new Round().Play(player, player)).ToBe(0);

        public static Action Winning(string player1, string player2) => () =>
        {
            Expect(new Round().Play(player1, player2)).ToBe(1);
        };

        public static Action Losing(string player1, string player2) => () =>
        {
            Expect(new Round().Play(player2, player1)).ToBe(2);
        };
    }
}