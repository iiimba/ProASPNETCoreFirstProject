using System.Collections.Generic;

namespace GOFPatternExamples.Observer.Push
{
    abstract class Subject
    {
        protected List<Observer> _observers = new List<Observer>();

        public abstract string State { get; set; }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(State);
            }
        }
    }

    class ConcreteSubject : Subject
    {
        /// <summary>
        /// Notify method can ve call on set operation
        /// </summary>
        public override string State { get; set; }
    }
}
