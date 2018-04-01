namespace RockPaperScissors
{
    public class Game
    {
        private int player1Score;
        private int player2Score;
        private readonly IGameListener listener;

        public Game(IGameListener listener)
        {
            this.listener = listener;
        }

        public void PlayRound(string player1, string player2)
        {
            var whichPlayer = new Round().Play(player1, player2);
            if (whichPlayer == 1) player1Score++;
            if (whichPlayer == 2) player2Score++;

            if (player1Score == 2)
            {
                listener.GameOver(1);
            }

            if (player2Score == 2)
            {
                listener.GameOver(2);
            }
        }
    }
}