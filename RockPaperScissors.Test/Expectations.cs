using System;

namespace RockPaperScissors.Test
{
    internal class Expectations
    {
        public static ExpectEquality<int, int> Expect(int result)
        {
            return new ExpectEquality<int, int>(result);
        }

        public static ExpectException Expect(Action action)
        {
            return new ExpectException(action);
        }
    }

    internal class ExpectException
    {
        private readonly Action action;

        public ExpectException(Action action)
        {
            this.action = action;
        }

        public void ToThrow<TException>()
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

            if (!(exception is TException))
            {
                throw new ExpectationFailedException($"expected {typeof(TException).Name}");
            }
        }
    }

    internal class ExpectEquality<TActual, TExpected>
    {
        private readonly TActual actual;

        public ExpectEquality(TActual actual)
        {
            this.actual = actual;
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