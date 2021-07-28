using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlainSpotters.Validators;

namespace PlainSpotters.ViewModels
{
    [Table("Sighting")]
    public class Sighting : IValidatableObject
    {
        private IRegistrationValidator _registrationValidator;
        private ISightingTimeSightedValidator _sightingTimeSightedValidator;

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var registrationValidationResult = RegistrationValidator.Validate(Registration);
            if (!string.IsNullOrEmpty(registrationValidationResult))
            {
                yield return new ValidationResult(registrationValidationResult, new[] { nameof(Registration) });
            }

            var timeSightedValidationResult = SightingTimeSightedValidator.Validate(TimeSighted);
            if (!string.IsNullOrEmpty(timeSightedValidationResult))
            {
                yield return new ValidationResult(timeSightedValidationResult, new[] { nameof(TimeSighted) });
            }
        }

        #endregion

        #region Properties

        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Location length can't be more than 255.")]
        public string Location { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Make length can't be more than 128.")]
        public string Make { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Model length can't be more than 8.")]
        public string Model { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Registration length can't be more than 8.")]
        public string Registration { get; set; }

        public IRegistrationValidator RegistrationValidator
        {
            private get => _registrationValidator ??= new RegistrationValidator();
            set => _registrationValidator = value;
        }

        public ISightingTimeSightedValidator SightingTimeSightedValidator
        {
            private get => _sightingTimeSightedValidator ??= new SightingTimeSightedValidator();
            set => _sightingTimeSightedValidator = value;
        }

        [DataType(DataType.Date)]
        [Display(Name = "Time Sighted")]
        public DateTime TimeSighted { get; set; }

        #endregion
    }
}