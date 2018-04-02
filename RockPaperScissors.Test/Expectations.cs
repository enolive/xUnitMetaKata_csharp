﻿using System;

namespace RockPaperScissors.Test
{
    internal class Expectations
    {
        private readonly RunState runState;

        public Expectations(RunState runState)
        {
            this.runState = runState;
        }

        public ExpectEquality<int, int> Expect(int result)
        {
            return new ExpectEquality<int, int>(runState, result);
        }
    }

    internal class ExpectEquality<TActual, TExpected>
    {
        private readonly RunState runState;
        private readonly TActual actual;

        public ExpectEquality(RunState runState, TActual actual)
        {
            this.runState = runState;
            this.actual = actual;
        }

        public void ToBe(TExpected expected, string message)
        {
            if (Equals(actual, expected))
            {
                runState.TestsPassed++;
                Console.WriteLine($"{message}: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine($"{message}: FAIL - expected {expected} but was {actual}");
            }
        }
    }
}