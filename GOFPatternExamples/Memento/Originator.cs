namespace GOFPatternExamples.Memento
{
    class Originator
    {
        /// <summary>
        /// This propery/field can be private
        /// </summary>
        public string State { get; set; }

        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }

        public Memento CreateMemento()
        {
            return new Memento(State);
        }
    }
}
