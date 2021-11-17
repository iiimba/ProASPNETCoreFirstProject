namespace GOFPatternExamples.Memento
{
    class Memento
    {
        public string State { get; init; }

        public Memento(string state)
        {
            State = state;
        }
    }

    /// <summary>
    /// Memento can be implemented via interface, hiding all state
    /// </summary>
    interface IMemento
    {
        void GetHistory();
    }
}
