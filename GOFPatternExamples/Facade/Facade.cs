namespace GOFPatternExamples.Facade
{
    class Facade
    {
        private SystemA _systemA = new SystemA();
        private SystemB _systemB = new SystemB();
        private SystemC _systemC = new SystemC();

        public void MethodA()
        {
            _systemA.OperationA();
            _systemB.OperationB();
        }

        public void MethodB()
        {
            _systemB.OperationB();
            _systemC.OperationC();
        }
    }
}
