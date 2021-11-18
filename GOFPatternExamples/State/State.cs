namespace GOFPatternExamples.State
{
    abstract class State
    {
        public abstract void Handle(Context context);
    }

    class ConcreteState1 : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteState2();
        }
    }

    class ConcreteState2 : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteState1();
        }
    }
}
