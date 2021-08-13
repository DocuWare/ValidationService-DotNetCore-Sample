using System;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Implementations.RootValueChecks
{
    /// <summary>
    /// Validates the store user is 'admin'
    /// </summary>
    public class TriggeredByAdminUserCheck : IRootValueCheck
    {
        ///<inheritdoc />
        public ValidationResult ValidateInput(InputValueModel inputValueModel)
        {
            if (inputValueModel == null)
            {
                return new ValidationResult { Success = false, ValidationMessage = "InputValueModel is null, not able to do validation!" };
            }

            return inputValueModel.UserName.Equals("admin",
                       StringComparison.CurrentCultureIgnoreCase) &&
                   inputValueModel.DialogType
                       ?.Equals("store",
                           StringComparison.InvariantCultureIgnoreCase) ==
                   true
                ? new ValidationResult { Success = true, ValidationMessage = "Everything is fine!" }
                : new ValidationResult { Success = false, ValidationMessage = "Oh no the value is not valid!" };

        }
    }
}