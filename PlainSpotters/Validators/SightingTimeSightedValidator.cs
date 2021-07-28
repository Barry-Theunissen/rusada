using System;

namespace PlainSpotters.Validators
{
    public interface ISightingTimeSightedValidator
    {
        string Validate(DateTime timeSighted);
    }

    public class SightingTimeSightedValidator : ISightingTimeSightedValidator
    {
        #region ISightingTimeSightedValidator Members

        public string Validate(DateTime timeSighted)
        {
            if (timeSighted >= DateTime.Now)
            {
                return "Must be in the past";
            }

            return string.Empty;
        }

        #endregion
    }
}