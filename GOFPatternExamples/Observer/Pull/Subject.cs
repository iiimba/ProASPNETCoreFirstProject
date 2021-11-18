using System.Collections.Generic;

namespace GOFPatternExamples.Observer.Pull
{
    abstract class Subject
    {
        protected readonly List<Observer> _observers = new List<Observer>();

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
                observer.Update();
            }
        }
    }

    class ConcreteSubject : Subject
    {
        /// <summary>
        /// Notify method can ve call on set operation
        /// </summary>
        public string State { get; set; }
    }
}
