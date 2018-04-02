namespace RockPaperScissors.Test
{
    internal class RunState
    {
        public int TestsPassed { get; set; }
        public int TestsFailed { get; set; }
        public int Total => TestsPassed + TestsFailed;
    }
}