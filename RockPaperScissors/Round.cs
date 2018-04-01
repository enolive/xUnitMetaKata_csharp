using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RockPaperScissors
{
    [Winning("Rock", "Scissors")]
    [Winning("Paper", "Rock")]
    [Winning("Scissors", "Paper")]
    public class Round
    {
        private readonly IEnumerable<(string, string)> winning;

        public Round()
        {
            winning = GetType()
                .GetCustomAttributes<WinningAttribute>()
                .Select(attribute => (attribute.Player1, attribute.Player2))
                .ToArray();
        }

        public PlayResult Play(string player1, string player2)
        {
            if (IsInvalid(player1, player2))
            {
                throw new InvalidMoveException();
            }

            if (IsWin(player1, player2))
            {
                return PlayResult.Player1;
            }

            if (IsWin(player2, player1))
            {
                return PlayResult.Player2;
            }

            return PlayResult.Draw;
        }

        private bool IsInvalid(string player1, string player2) =>
            IsInvalid(player1) || IsInvalid(player2);

        private bool IsInvalid(string player) =>
            winning.All(w => w.Item1 != player);

        private bool IsWin(string player1, string player2) =>
            winning.Any(w => w.Equals((player1, player2)));
    }
}
