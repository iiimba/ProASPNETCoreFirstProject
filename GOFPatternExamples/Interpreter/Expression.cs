namespace GOFPatternExamples.Interpreter
{
    abstract class Expression
    {
        public abstract void Interpret(Context context);
    }

    class TerminalExpression : Expression
    {
        public override void Interpret(Context context)
        {
            context.Result = context.Source[context.Position] == context.Vocabulary;
        }
    }

    class NonTerminalExpression : Expression
    {
        private Expression _terminalExpression;
        private Expression _nonTerminalExpression;

        public override void Interpret(Context context)
        {
            if (context.Position < context.Source.Length)
            {
                _terminalExpression = new TerminalExpression();
                _terminalExpression.Interpret(context);
                context.Position++;

                if (context.Result)
                {
                    _nonTerminalExpression = new NonTerminalExpression();
                    _nonTerminalExpression.Interpret(context);
                }
            }
        }
    }
}
