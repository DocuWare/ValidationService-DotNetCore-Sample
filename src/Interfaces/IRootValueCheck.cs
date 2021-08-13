using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Interfaces
{
    /// <summary>
    /// Validates the root elements of the input model
    /// </summary>
    public interface IRootValueCheck
    {
        /// <summary>
        /// Does the validation/check
        /// </summary>
        /// <param name="inputValueModel">The input model retrieved from DocuWare Validation</param>
        /// <returns></returns>
        ValidationResult ValidateInput(InputValueModel inputValueModel);
    }
}