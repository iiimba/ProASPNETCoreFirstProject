using System;

namespace GOFPatternExamples.FactoryMethod
{
    abstract class Transport
    {
        public abstract void Deliver();
    }

    class Truck : Transport
    {
        public override void Deliver()
        {
            Console.WriteLine($"Delivered by {nameof(Truck)}");
        }
    }

    class Ship : Transport
    {
        public override void Deliver()
        {
            Console.WriteLine($"Delivered by {nameof(Ship)}");
        }
    }
}
