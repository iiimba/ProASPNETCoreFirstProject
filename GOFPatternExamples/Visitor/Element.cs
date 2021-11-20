using System;

namespace GOFPatternExamples.Visitor
{
    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }

    class ConcreteElement1 : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElement1(this);
        }

        public void VisitElement1()
        {
            Console.WriteLine($"{nameof(ConcreteElement1)} visited");
        }
    }

    class ConcreteElement2 : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElement2(this);
        }

        public void VisitElement2()
        {
            Console.WriteLine($"{nameof(ConcreteElement2)} visited");
        }
    }
}
