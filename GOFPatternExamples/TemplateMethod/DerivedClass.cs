using System;

namespace GOFPatternExamples.TemplateMethod
{
    class DerivedClass : AbstractClass
    {
        public override void Method1()
        {
            Console.WriteLine(nameof(Method1));
        }

        public override void Method2()
        {
            Console.WriteLine(nameof(Method2));
        }
    }
}
