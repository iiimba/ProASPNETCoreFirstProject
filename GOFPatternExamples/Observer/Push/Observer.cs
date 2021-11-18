namespace GOFPatternExamples.Observer.Push
{
    abstract class Observer
    {
        public abstract void Update(string state);
    }

    class ConcreteObserver : Observer
    {
        public string State { get; private set; }

        public override void Update(string state)
        {
            State = state;
            System.Console.WriteLine(State);
        }
    }
}
