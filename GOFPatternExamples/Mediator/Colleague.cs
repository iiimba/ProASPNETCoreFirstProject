using System;

namespace GOFPatternExamples.Mediator
{
    abstract class Colleague
    {
        protected Mediator _mediator;

        public Colleague(Mediator mediator)
        {
            _mediator = mediator;
        }
    }

    class Colleague1 : Colleague
    {
        public Colleague1(Mediator mediator) : base(mediator)
        {

        }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"Notified: {nameof(Colleague1)} with message: {message}");
        }
    }

    class Colleague2 : Colleague
    {
        public Colleague2(Mediator mediator) : base(mediator)
        {

        }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"Notified: {nameof(Colleague2)} with message: {message}");
        }
    }
}
