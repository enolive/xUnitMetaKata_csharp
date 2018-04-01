using System;
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
            if (winning.All(w => w.Item1 != player1))
            {
                throw new InvalidMoveException();
            }
            if (winning.All(w => w.Item1 != player2))
            {
                throw new InvalidMoveException();
            }
            if (winning.Any(w => player1 == w.Item1 && player2 == w.Item2))
            {
                return PlayResult.Player1;
            }
            if (winning.Any(w => player2 == w.Item1 && player1 == w.Item2))
            {
                return PlayResult.Player2;
            }

            return PlayResult.Draw;
        }
    }
}
