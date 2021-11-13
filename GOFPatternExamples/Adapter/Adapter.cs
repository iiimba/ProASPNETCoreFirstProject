namespace GOFPatternExamples.Adapter
{
    /// <summary>
    /// Class adapter
    /// </summary>
    class ClassAdapter : Adaptee, ITarget
    {
        public void Request()
        {
            SpecificMethod();
        }
    }

    /// <summary>
    /// Object adapter
    /// </summary>
    class ObjectAdapter : Target
    {
        private Adaptee _adaptee = new Adaptee();

        public override void Request()
        {
            _adaptee.SpecificMethod();
        }
    }
}
