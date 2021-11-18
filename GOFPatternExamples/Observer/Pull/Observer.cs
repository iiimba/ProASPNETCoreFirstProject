namespace GOFPatternExamples.Observer.Pull
{
    abstract class Observer
    {
        public abstract void Update();
    }

    class ConcreteObserver : Observer
    {
        private readonly ConcreteSubject _subject;
        
        public ConcreteObserver(ConcreteSubject subject)
        {
            _subject = subject;
        }

        public string State { get; private set; }

        public override void Update()
        {
            State = _subject.State;
            System.Console.WriteLine(State);
        }
    }
}
