namespace GOFPatternExamples.State.RealExample
{
    /// <summary>
    /// This example is not very good, media player is more suitable
    /// </summary>
    abstract class CardState
    {
        public decimal Balance { get; protected set; }

        public void AddBalance(decimal balance)
        {
            Balance += balance;
        }

        public abstract void ChangeCardState(Account account);

        public abstract decimal InterestOnLoan();
    }

    class SimpleState : CardState
    {
        public override void ChangeCardState(Account account)
        {
            if (Balance > 1_000_000)
            {
                account.State = new GoldState();
            }
            else if (Balance < 1_000_000 && Balance > 100_000)
            {
                account.State = new SilverState();
            }
        }

        public override decimal InterestOnLoan()
        {
            return 8.3m;
        }
    }

    class SilverState : CardState
    {
        public override void ChangeCardState(Account account)
        {
            if (Balance > 1_000_000)
            {
                account.State = new GoldState();
            }
            else if (Balance < 100_000)
            {
                account.State = new SimpleState();
            }
        }

        public override decimal InterestOnLoan()
        {
            return 5.7m;
        }
    }

    class GoldState : CardState
    {
        public override void ChangeCardState(Account account)
        {
            if (Balance < 1_000_000 && Balance > 100_000)
            {
                account.State = new SilverState();
            }
            else if (Balance < 100_000)
            {
                account.State = new SimpleState();
            }
        }

        public override decimal InterestOnLoan()
        {
            return 3.1m;
        }
    }
}
