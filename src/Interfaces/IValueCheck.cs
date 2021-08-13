using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Interfaces
{
    /// <summary>
    /// Validates the value elements of the input model
    /// </summary>
    public interface IValueCheck
    {
        /// <summary>
        /// Does the validation/check
        /// </summary>
        /// <param name="valueModel">The input model values retrieved from DocuWare Validation</param>
        /// <returns></returns>
        ValidationResult ValidateInput(ValueModel valueModel);
    }
}