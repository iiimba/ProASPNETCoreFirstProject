namespace TestProject
{
    public class Calculator
    {
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }
    }

    public class CalculatorFixture
    {
        public Calculator Calculator { get; init; }

        public CalculatorFixture()
        {
            Calculator = new Calculator();
        }
    }
}
