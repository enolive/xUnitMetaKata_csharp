using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Round
    {
        public PlayResult Play(string player1, string player2)
        {
            if (player1 == "Rock")
            {
                if (player2 == "Scissors")
                    return PlayResult.Player1;
                if (player2 == "Paper")
                    return PlayResult.Player2;
                if (player2 == "Rock")
                    return PlayResult.Draw;
            }
            if (player1 == "Paper")
            {
                if (player2 == "Rock")
                    return PlayResult.Player1;
                if (player2 == "Scissors")
                    return PlayResult.Player2;
                if (player2 == "Paper")
                    return PlayResult.Draw;
            }
            if (player1 == "Scissors")
            {
                if (player2 == "Paper")
                    return PlayResult.Player1;
                if (player2 == "Rock")
                    return PlayResult.Player2;
                if (player2 == "Scissors")
                    return PlayResult.Draw;
            }
            throw new InvalidMoveException();
        }
    }
}
