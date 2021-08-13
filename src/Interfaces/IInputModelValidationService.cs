using System.Threading.Tasks;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Interfaces
{
    /// <summary>
    /// Handling all validations
    /// </summary>
    public interface IInputModelValidationService
    {
        /// <summary>
        /// Does simple checks
        /// </summary>
        /// <param name="inputValueModel">Input value model retrieved by DocuWare</param>
        /// <returns></returns>
        Task<ReturnValueModel> CheckValuesSimple(InputValueModel inputValueModel);

        /// <summary>
        /// Does all checks on the input value model root
        /// </summary>
        /// <param name="inputValueModel">Input value model retrieved by DocuWare</param>
        /// <returns></returns>
        Task<ReturnValueModel> CheckRootValuesSimple(InputValueModel inputValueModel);

        /// <summary>
        /// Here some validation against a / the DocuWare System is done
        /// </summary>
        /// <param name="inputValueModel">Input value model retrieved by DocuWare</param>
        Task<ReturnValueModel> CheckValuesAgainstDocuWareSystem(InputValueModel inputValueModel);

        /// <summary>
        /// Debug purpose, just parses the input to console
        /// </summary>
        /// <param name="inputValueModel">Input value model retrieved by DocuWare</param>
        Task<ReturnValueModel> WriteAllValuesToConsole(InputValueModel inputValueModel);
    }
}