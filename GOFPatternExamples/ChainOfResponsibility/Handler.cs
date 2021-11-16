using System;

namespace GOFPatternExamples.ChainOfResponsibility
{
    abstract class Handler
    {
        protected Handler _successor;

        public void SetSuccessor(Handler successor)
        {
            _successor = successor;
        }

        public abstract void Handle(int code);
    }

    class ExceptionHandler1 : Handler
    {
        public override void Handle(int code)
        {
            if (code == 1)
            {
                Console.WriteLine($"Handled by {nameof(ExceptionHandler1)}");
            }
            else if (_successor != null)
            {
                _successor.Handle(code);
            }
        }
    }

    class ExceptionHandler2 : Handler
    {
        public override void Handle(int code)
        {
            if (code == 2)
            {
                Console.WriteLine($"Handled by {nameof(ExceptionHandler2)}");
            }
            else if (_successor != null)
            {
                _successor.Handle(code);
            }
        }
    }

    class ExceptionHandler3 : Handler
    {
        public override void Handle(int code)
        {
            if (code == 3)
            {
                Console.WriteLine($"Handled by {nameof(ExceptionHandler3)}");
            }
            else if (_successor != null)
            {
                _successor.Handle(code);
            }
        }
    }
}
