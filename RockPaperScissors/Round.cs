using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RockPaperScissors
{
    [Winning("Rock", "Scissors")]
    [Winning("Paper", "Rock")]
    [Winning("Scissors", "Paper")]
    public class Round
    {
        private readonly IEnumerable<(string, string)> winning;
        private readonly IEnumerable<Rule> rules;

        public Round()
        {
            winning = GetType()
                .GetCustomAttributes<WinningAttribute>()
                .Select(attribute => (attribute.Player1, attribute.Player2))
                .ToArray();
            rules = new[]
            {
                new Rule(IsInvalid, () => throw new InvalidMoveException()),
                new Rule(WinsPlayer1, PlayResult.Player1),
                new Rule(WinsPlayer2, PlayResult.Player2),
                new Rule(Otherwise, PlayResult.Draw)
            };
        }

        public PlayResult Play(string player1, string player2) => rules
            .Where(rule => rule.IsFullfilledFor(player1, player2))
            .Select(c => c.Result)
            .First();

        private static bool Otherwise(string player1, string player2) => true;

        private bool IsInvalid(string player1, string player2) => IsInvalid(player1) || IsInvalid(player2);

        private bool WinsPlayer2(string player1, string player2) => WinsPlayer1(player2, player1);

        private bool IsInvalid(string player) =>
            winning.All(w => w.Item1 != player);

        private bool WinsPlayer1(string player1, string player2) =>
            winning.Any(w => w.Equals((player1, player2)));

        private class Rule
        {
            private readonly Func<PlayResult> result;
            public Func<string, string, bool> IsFullfilledFor { get; }

            public PlayResult Result => result();

            public Rule(Func<string, string, bool> condition, Func<PlayResult> result)
            {
                this.result = result;
                IsFullfilledFor = condition;
            }

            public Rule(Func<string, string, bool> condition, PlayResult result)
                : this(condition, () => result)
            {
            }
        }
    }
}