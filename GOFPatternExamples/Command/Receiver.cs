using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOFPatternExamples.Command
{
    /// <summary>
    /// Receiver
    /// </summary>
    class ArithmeticUnit
    {
        public double Result { get; private set; }

        public void Run(char operation, double operand)
        {
            switch (operation)
            {
                case '+':
                    Result += operand;
                    break;
                case '-':
                    Result -= operand;
                    break;
                case '*':
                    Result *= operand;
                    break;
                case '/':
                    Result /= operand;
                    break;
                default:
                    break;
            }
        }
    }
}
