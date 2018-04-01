namespace RockPaperScissors
{
    public interface IGameListener
    {
        void GameOver(PlayResult winner);
    }
}