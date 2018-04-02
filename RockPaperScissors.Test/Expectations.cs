using System;

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

        public ExpectException Expect(Action action)
        {
            return new ExpectException(runState, action);
        }
    }

    internal class ExpectException
    {
        private readonly RunState runState;
        private readonly Action action;

        public ExpectException(RunState runState, Action action)
        {
            this.runState = runState;
            this.action = action;
        }

        public void ToThrow<TException>(string message)
        {
            Exception exception = null;
            try
            {
                action();
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception is TException)
            {
                runState.TestsPassed++;
                Console.WriteLine($"{message}: PASS");
            }
            else
            {
                runState.TestsFailed++;
                Console.WriteLine($"{message}: FAIL - expected {typeof(TException).Name}");
            }
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

        public void ToBe(TExpected expected)
        {
            if (!Equals(actual, expected))
            {
                throw new ExpectationFailedException($"expected {expected} but was {actual}");
            }
        }
    }
}