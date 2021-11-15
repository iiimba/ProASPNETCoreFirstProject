using System;

namespace GOFPatternExamples.Flyweight
{
    abstract class Flyweight
    {
        public abstract void Operation(object o);
    }

    class ConcreteFlyweight : Flyweight
    {
        private readonly object _intrisicState;

        public ConcreteFlyweight(object intrisicState)
        {
            _intrisicState = intrisicState;
        }

        /// <param name="o">extrinsic state</param>
        public override void Operation(object o)
        {
            Console.WriteLine($"{nameof(ConcreteFlyweight)}, {o}");
        }
    }

    class UnsharedFlyweight : Flyweight
    {
        private object _state;

        public override void Operation(object o)
        {
            _state = o;
            Console.WriteLine($"{nameof(UnsharedFlyweight)}, {o}");
        }
    }
}
