namespace GOFPatternExamples.Adapter
{
    interface ITarget
    {
        void Request();
    }

    abstract class Target
    {
        public abstract void Request();
    }
}
