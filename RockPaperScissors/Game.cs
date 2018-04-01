using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public class Game
    {
        private const int RequiredRounds = 2;

        private readonly IGameListener listener;
        private readonly List<PlayResult> score = new List<PlayResult>();

        public Game(IGameListener listener)
        {
            this.listener = listener;
        }

        public void PlayRound(PlayChoice player1, PlayChoice player2)
        {
            try
            {
                score.Add(GetScore(player1, player2));
            }
            catch (InvalidMoveException)
            {
            }

            var whoWins = LookupWins()
                .Where(grouping => grouping.Count() == RequiredRounds)
                .Select(grouping => grouping.Key)
                .FirstOrDefault();

            if (whoWins != PlayResult.Draw)
            {
                listener.GameOver((int) whoWins);
            }
        }

        private ILookup<PlayResult, PlayResult> LookupWins() =>
            score
                .Where(s => s != PlayResult.Draw)
                .ToLookup(s => s);

        private static PlayResult GetScore(PlayChoice player1, PlayChoice player2) =>
            new Round().Play(player1, player2);
    }
}