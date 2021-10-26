using Moq;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferrerToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(v => v.IsValid("x")).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.Is<string>(arg => arg == "x"))).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsInRange("a", "y", Range.Exclusive))).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsIn(new[] { "abcx", "b", "c", "x" }))).Returns(true);
            mockValidator.Setup(v => v.IsValid(It.IsRegex("^a[a-z]+x$"))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "abcx"
            };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplicationsOutParameter()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var isValid = true;

            mockValidator.Setup(v => v.IsValid(It.IsAny<string>(), out isValid));
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 19_999, Age = 42 };
            
            var decision = sut.EvaluateUsingOut(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }
    }
}
