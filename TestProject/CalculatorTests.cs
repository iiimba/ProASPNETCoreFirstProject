using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject
{
    public class CalculatorTests : IClassFixture<CalculatorFixture>
    {
        private readonly Calculator _calculator;
        private readonly CalculatorFixture _calculatorFixture;

        public CalculatorTests(CalculatorFixture calculatorFixture)
        {
            _calculator = new Calculator();
            _calculatorFixture = calculatorFixture;
        }

        [Fact]
        [Trait("TestTrait", "Test")]
        public void CanAdd()
        {
            int value1 = 1;
            int value2 = 2;

            var result = _calculator.Add(value1, value2);

            Assert.Equal(3, result);
        }

        [Theory(Skip = "Some reason")]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void CanAddTheory(int value1, int value2, int expected)
        {
            var result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void CanAddTheoryClassData(int value1, int value2, int expected)
        {
            var result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void CanAddTheoryMemberDataProperty(int value1, int value2, int expected)
        {
            var result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -4, -6, -10 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue },
            };

        [Theory]
        [MemberData(nameof(GetData), parameters: 3)]
        public void CanAddTheoryMemberDataMethod(int value1, int value2, int expected)
        {
            var result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -4, -6, -10 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue },
            };

            return allData.Take(numTests);
        }

        [Theory]
        [MemberData(nameof(CalculatorData.Data), MemberType = typeof(CalculatorData))]
        public void CanAddTheoryMemberDataMethodFromAnotherMethod(int value1, int value2, int expected)
        {
            var result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }
    }

    public class CalculatorData
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -4, -6, -10 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue }
            };
    }
}
