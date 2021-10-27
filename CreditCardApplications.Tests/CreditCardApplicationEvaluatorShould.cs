using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        private Mock<IFrequentFlyerNumberValidator> mockValidator;
        private CreditCardApplicationEvaluator sut;

        public CreditCardApplicationEvaluatorShould()
        {
            mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.SetupAllProperties();
            mockValidator.Setup(v => v.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            sut = new CreditCardApplicationEvaluator(mockValidator.Object);
        }

        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            mockValidator.DefaultValue = DefaultValue.Mock;
            var application = new CreditCardApplication { Age = 19 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferrerToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            //mockValidator.Setup(v => v.IsValid("x")).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.Is<string>(arg => arg == "x"))).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsInRange("a", "y", Range.Exclusive))).Returns(true);
            //mockValidator.Setup(v => v.IsValid(It.IsIn(new[] { "abcx", "b", "c", "x" }))).Returns(true);
            mockValidator.Setup(v => v.IsValid(It.IsRegex("^a[a-z]+x$"))).Returns(true);
            mockValidator.DefaultValue = DefaultValue.Mock;

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
            var isValid = true;

            mockValidator.Setup(v => v.IsValid(It.IsAny<string>(), out isValid));
            var application = new CreditCardApplication { GrossAnnualIncome = 19_999, Age = 42 };

            var decision = sut.EvaluateUsingOut(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferWhenLicenseKeyExpired()
        {
            //mockValidator.Setup(v => v.LicenseKey).Returns("EXPIRED");
            //mockValidator.Setup(v => v.LicenseKey).Returns(GetLicenseKeyExpiryString);

            //var mockLicenseData = new Mock<ILicenseData>();
            //mockLicenseData.Setup(v => v.LicenseKey).Returns("EXPIRED");

            //var mockServiceInformation = new Mock<IServiceInformation>();
            //mockServiceInformation.Setup(v => v.License).Returns(mockLicenseData.Object);

            mockValidator.Setup(v => v.ServiceInformation.License.LicenseKey).Returns("EXPIRED");

            var application = new CreditCardApplication { Age = 42 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferrerToHuman, decision);
        }

        [Fact]
        public void UseDeyailedLookupForOlderApplications()
        {
            //mockValidator.SetupAllProperties();
            //mockValidator.SetupProperty(v => v.ValidationMode);

            var application = new CreditCardApplication { Age = 30 };

            var decision = sut.Evaluate(application);

            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        [Fact]
        public void ValidateFrequentFlyerNumberForLowIncomeApplications()
        {
            var application = new CreditCardApplication { FrequentFlyerNumber = "q" };

            var decision = sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once, "FrequentFlyerNumber should be validated");
        }

        [Fact]
        public void NotValidateFrequentFlyerNumberForHighIncomeApplications()
        {
            var application = new CreditCardApplication { GrossAnnualIncome = 200_000 };

            var decision = sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CheckLicenseKeyForLowIncomeApplications()
        {
            var application = new CreditCardApplication();

            var decision = sut.Evaluate(application);

            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey);
        }

        [Fact]
        public void SetDetailedLookupForOlderApplications()
        {
            var application = new CreditCardApplication { Age = 30 };

            var decision = sut.Evaluate(application);

            //mockValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed);
            mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>());
            //mockValidator.VerifyNoOtherCalls();
        }

        [Fact]
        public void RefererWhenFrequentFlyerValidationError()
        {
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Throws(new Exception("Some exception"));

            var application = new CreditCardApplication { Age = 42 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferrerToHuman, decision);
        }

        [Fact]
        public void IncrementLookupCount()
        {
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true).Raises(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);

            var application = new CreditCardApplication { FrequentFlyerNumber = "x", Age = 25 };

            sut.Evaluate(application);

            //mockValidator.Raise(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);

            Assert.Equal(1, sut.ValidatorLookupCount);
        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications_ReturnValuesSequence()
        {
            mockValidator.SetupSequence(v => v.IsValid(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            var application = new CreditCardApplication { Age = 25 };

            var firstDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferrerToHuman, firstDecision);

            var secondDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications_MultipleCallSequence()
        {
            var frequentFlyerNumbersPassed = new List<string>();
            mockValidator.Setup(x => x.IsValid(Capture.In(frequentFlyerNumbersPassed)));

            var application1 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "a" };
            var application2 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "b" };
            var application3 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "c" };

            sut.Evaluate(application1);
            sut.Evaluate(application2);
            sut.Evaluate(application3);

            Assert.Equal(new List<string> { "a", "b", "c" }, frequentFlyerNumbersPassed);
        }

        [Fact]
        public void ReferFraudRisk()
        {
            var mockFraudLookup = new Mock<FraudLookup>();
            //mockFraudLookup.Setup(f => f.IsFraudRisk(It.IsAny<CreditCardApplication>())).Returns(true);
            mockFraudLookup.Protected().Setup<bool>("CheckApplication", ItExpr.IsAny<CreditCardApplication>()).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object, mockFraudLookup.Object);

            var application = new CreditCardApplication();

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferrerToHumanFraudRisk, decision);
        }

        [Fact]
        public void LinqToMocks()
        {
            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            var mockValidator = Mock.Of<IFrequentFlyerNumberValidator>(validator => 
                validator.ServiceInformation.License.LicenseKey == "OK"
                && validator.IsValid(It.IsAny<string>()) == true);

            var sut = new CreditCardApplicationEvaluator(mockValidator);

            var application = new CreditCardApplication { Age = 25 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        private string GetLicenseKeyExpiryString()
        {
            // read from file, for example
            return "EXPIRED";
        }
    }
}
