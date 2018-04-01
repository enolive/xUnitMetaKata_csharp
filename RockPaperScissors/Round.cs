using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RockPaperScissors
{
    [Winning(PlayChoice.Rock, PlayChoice.Scissors)]
    [Winning(PlayChoice.Paper, PlayChoice.Rock)]
    [Winning(PlayChoice.Scissors, PlayChoice.Paper)]
    public class Round
    {
        private readonly IEnumerable<(PlayChoice, PlayChoice)> winning;
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

        public PlayResult Play(PlayChoice player1, PlayChoice player2) => rules
            .Where(rule => rule.IsFullfilledFor(player1, player2))
            .Select(c => c.Result)
            .First();

        private static bool Otherwise(PlayChoice player1, PlayChoice player2) => true;

        private bool IsInvalid(PlayChoice player1, PlayChoice player2) => IsInvalid(player1) || IsInvalid(player2);

        private bool WinsPlayer2(PlayChoice player1, PlayChoice player2) => WinsPlayer1(player2, player1);

        private bool IsInvalid(PlayChoice player) =>
            winning.All(w => w.Item1 != player);

        private bool WinsPlayer1(PlayChoice player1, PlayChoice player2) =>
            winning.Any(w => w.Equals((player1, player2)));

        private class Rule
        {
            private readonly Func<PlayResult> result;
            public Func<PlayChoice, PlayChoice, bool> IsFullfilledFor { get; }

            public PlayResult Result => result();

            public Rule(Func<PlayChoice, PlayChoice, bool> condition, Func<PlayResult> result)
            {
                this.result = result;
                IsFullfilledFor = condition;
            }

            public Rule(Func<PlayChoice, PlayChoice, bool> condition, PlayResult result)
                : this(condition, () => result)
            {
            }
        }
    }

    public enum PlayChoice
    {
        Rock,
        Scissors,
        Paper
    }
}