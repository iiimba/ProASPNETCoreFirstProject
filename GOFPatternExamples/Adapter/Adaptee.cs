using System;

namespace GOFPatternExamples.Adapter
{
    class Adaptee
    {
        public void SpecificMethod()
        {
            Console.WriteLine(nameof(Adaptee.SpecificMethod));
        }
    }
}
