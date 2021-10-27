using System;

namespace CreditCardApplications
{
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        public IServiceInformation ServiceInformation => throw new NotImplementedException();

        public ValidationMode ValidationMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler ValidatorLookupPerformed;

        //public string LicenseKey => throw new NotImplementedException();

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new NotImplementedException();
        }

        public void IsValid(string frequentFlyerNumber, out bool isValidFrequentFlyerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
