using System;

namespace RockPaperScissors
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WinningAttribute : Attribute
    {
        public string Player1 { get; }
        public string Player2 { get; }

        public WinningAttribute(string player1, string player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }
}