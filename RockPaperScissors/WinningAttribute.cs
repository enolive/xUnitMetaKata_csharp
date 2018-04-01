using System;

namespace RockPaperScissors
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WinningAttribute : Attribute
    {
        public PlayChoice Player1 { get; }
        public PlayChoice Player2 { get; }

        public WinningAttribute(PlayChoice player1, PlayChoice player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }
}