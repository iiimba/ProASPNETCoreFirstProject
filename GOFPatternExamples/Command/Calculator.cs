namespace GOFPatternExamples.Command
{
    /// <summary>
    /// Client
    /// </summary>
    class Calculator
    {
        private ControlUnit _invoker;
        private ArithmeticUnit _receiver;

        public Calculator()
        {
            _invoker = new ControlUnit();
            _receiver = new ArithmeticUnit();
        }

        public double Add(double operand)
        {
            return Run(new Add(_receiver, operand));
        }

        public double Sub(double operand)
        {
            return Run(new Sub(_receiver, operand));
        }

        public double Mul(double operand)
        {
            return Run(new Mul(_receiver, operand));
        }

        public double Div(double operand)
        {
            return Run(new Div(_receiver, operand));
        }

        public double Undo()
        {
            _invoker.Undo();
            return _receiver.Result;
        }

        public double Redo()
        {
            _invoker.Redo();
            return _receiver.Result;
        }

        private double Run(Command command)
        {
            _invoker.StoreCommand(command);
            _invoker.ExecuteCommand();
            return _receiver.Result;
        }
    }
}
