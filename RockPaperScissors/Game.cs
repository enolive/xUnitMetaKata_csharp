using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public class Game
    {
        private readonly IGameListener listener;
        private readonly List<PlayResult> score = new List<PlayResult>();

        public Game(IGameListener listener)
        {
            this.listener = listener;
        }

        public void PlayRound(PlayChoice player1, PlayChoice player2)
        {
            AddScore(player1, player2);
            NotifyListener();
        }

        private void NotifyListener()
        {
            var whoWins = LookupWins()
                .Where(PlayerHasWonRequiredRounds)
                .Select(WhichPlayer)
                .FirstOrDefault();

            if (whoWins != PlayResult.Draw)
            {
                listener.GameOver(whoWins);
            }
        }

        private void AddScore(PlayChoice player1, PlayChoice player2)
        {
            try
            {
                score.Add(GetScore(player1, player2));
            }
            catch (InvalidMoveException)
            {
            }
        }

        private ILookup<PlayResult, PlayResult> LookupWins() =>
            score
                .Where(s => s != PlayResult.Draw)
                .ToLookup(s => s);

        private static PlayResult WhichPlayer(IGrouping<PlayResult, PlayResult> grouping) => grouping.Key;

        private static bool PlayerHasWonRequiredRounds(IGrouping<PlayResult, PlayResult> grouping) =>
            grouping.Count() == 2;

        private static PlayResult GetScore(PlayChoice player1, PlayChoice player2) =>
            new Round().Play(player1, player2);
    }
}