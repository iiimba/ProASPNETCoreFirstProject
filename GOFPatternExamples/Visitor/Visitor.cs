namespace GOFPatternExamples.Visitor
{
    abstract class Visitor
    {
        public abstract void VisitConcreteElement1(ConcreteElement1 element);

        public abstract void VisitConcreteElement2(ConcreteElement2 element);
    }

    class ConcreteVisitor : Visitor
    {
        public override void VisitConcreteElement1(ConcreteElement1 element)
        {
            element.VisitElement1();
        }

        public override void VisitConcreteElement2(ConcreteElement2 element)
        {
            element.VisitElement2();
        }
    }
}
