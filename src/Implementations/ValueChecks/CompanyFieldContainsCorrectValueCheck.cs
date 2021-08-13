using System;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Implementations.ValueChecks
{
    /// <summary>
    /// Validates the value in field 'COMPANY'
    /// </summary>
    public class CompanyFieldContainsCorrectValueCheck : IValueCheck
    {
        ///<inheritdoc />
        public ValidationResult ValidateInput(ValueModel valueModel)
        {
            if (valueModel == null)
            {
                return new ValidationResult { Success = false, ValidationMessage = "ValueModel is null, not able to do validation!" };
            }

            // the Field "Company" contains the value "Peters Engineering"
            return valueModel.FieldName.Equals("Company",
                       StringComparison.CurrentCultureIgnoreCase) &&
                   valueModel.Item?.ToString()
                       ?.Equals("Peters Engineering", 
                           StringComparison.InvariantCultureIgnoreCase) ==
                   true
                ? new ValidationResult { Success = true, ValidationMessage = "Everything is fine!" }
                : new ValidationResult { Success = false, ValidationMessage = "Value is not valid!" }; 
        }
    }
}