using System;

namespace GOFPatternExamples.State
{
    class Context
    {
        private State _state;
        
        public Context(State state)
        {
            State = state;
        }

        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
                Console.WriteLine(_state.GetType().Name);
            }
        }

        public void Request()
        {
            State.Handle(this);
        }
    }
}
