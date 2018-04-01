using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public class Round
    {
        private readonly IEnumerable<(string, string)> winning = new[]
        {
            ("Rock", "Scissors"),
            ("Paper", "Rock"),
            ("Scissors", "Paper")
        };

        public PlayResult Play(string player1, string player2)
        {
            if (IsInvalid(player1) || IsInvalid(player2))
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

        private bool IsInvalid(string player)
        {
            return winning.All(w => w.Item1 != player);
        }

        private bool IsWin(string player1, string player2)
        {
            var tuple = (player1, player2);
            return winning.Any(w => w.Equals(tuple));
        }
    }
}
