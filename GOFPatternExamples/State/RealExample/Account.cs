namespace GOFPatternExamples.State.RealExample
{
    class Account
    {
        public CardState State { get; set; } = new SimpleState();

        public decimal Balance
        {
            get { return State.Balance; }
        }

        public void AddBalance(decimal balance)
        {
            State.AddBalance(balance);
        }

        public decimal InterestOnLoan
        {
            get { return State.InterestOnLoan(); }
        }

        public void RequestToChangeCardState()
        {
            State.ChangeCardState(this);
        }
    }
}
