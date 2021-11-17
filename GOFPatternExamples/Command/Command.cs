namespace GOFPatternExamples.Command
{
    abstract class Command
    {
        protected ArithmeticUnit _receiver;
        protected double _operand;

        public Command(ArithmeticUnit receiver, double operand)
        {
            _receiver = receiver;
            _operand = operand;
        }

        public abstract void Execute();

        public abstract void UnExecute();
    }

    class Add : Command
    {
        public Add(ArithmeticUnit receiver, double operand)
            : base(receiver, operand)
        {

        }

        public override void Execute()
        {
            _receiver.Run('+', _operand);
        }

        public override void UnExecute()
        {
            _receiver.Run('-', _operand);
        }
    }

    class Sub : Command
    {
        public Sub(ArithmeticUnit receiver, double operand)
            : base(receiver, operand)
        {

        }

        public override void Execute()
        {
            _receiver.Run('-', _operand);
        }

        public override void UnExecute()
        {
            _receiver.Run('+', _operand);
        }
    }

    class Mul : Command
    {
        public Mul(ArithmeticUnit receiver, double operand)
            : base(receiver, operand)
        {

        }

        public override void Execute()
        {
            _receiver.Run('*', _operand);
        }

        public override void UnExecute()
        {
            _receiver.Run('/', _operand);
        }
    }

    class Div : Command
    {
        public Div(ArithmeticUnit receiver, double operand)
            : base(receiver, operand)
        {

        }

        public override void Execute()
        {
            _receiver.Run('/', _operand);
        }

        public override void UnExecute()
        {
            _receiver.Run('*', _operand);
        }
    }
}
