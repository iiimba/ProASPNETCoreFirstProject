namespace GOFPatternExamples.TemplateMethod
{
    abstract class AbstractClass
    {
        public void TemplateMethod()
        {
            Method1();

            Method2();
        }

        public abstract void Method1();

        public abstract void Method2();
    }
}
