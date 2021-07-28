using System;

namespace PlainSpotters.Validators
{
    public interface IRegistrationValidator
    {
        string Validate(string registration);
    }

    public class RegistrationValidator : IRegistrationValidator
    {
        #region IRegistrationValidator Members

        public string Validate(string registration)
        {
            if (!registration.Contains("-"))
            {
                return "Should contain hyphen";
            }

            var registrationSplit = registration.Split("-", StringSplitOptions.RemoveEmptyEntries);
            if (registrationSplit.Length > 2)
            {
                return "Too many hyphens";
            }

            if (registrationSplit.Length == 1)
            {
                return "Must contain a prefix and a suffix separated by a hyphen";
            }

            if (registrationSplit[0].Length == 0)
            {
                return "No registration prefix";
            }

            if (registrationSplit[0].Length > 2)
            {
                return "Prefix too big";
            }

            if (registrationSplit[1].Length == 0)
            {
                return "No suffix";
            }

            if (registrationSplit[1].Length > 5)
            {
                return "Suffix too big";
            }

            return string.Empty;
        }

        #endregion
    }
}