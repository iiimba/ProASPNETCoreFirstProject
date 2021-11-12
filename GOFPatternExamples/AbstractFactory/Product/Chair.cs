using System;

namespace GOFPatternExamples.AbstractFactory.Product
{
    abstract class Chair
    {
        /// <summary>
        /// This method can be omitted in our example
        /// </summary>
        /// <param name="sofa"></param>
        public abstract void Interact(Sofa sofa);
    }

    class VictorianChair : Chair
    {
        public override void Interact(Sofa sofa)
        {
            Console.WriteLine($"Interaction between: {GetType().Name} and {sofa.GetType().Name}");
        }
    }

    class ModernChair : Chair
    {
        public override void Interact(Sofa sofa)
        {
            Console.WriteLine($"Interaction between: {GetType().Name} and {sofa.GetType().Name}");
        }
    }
}
