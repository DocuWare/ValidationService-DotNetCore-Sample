using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DocuWare.Platform.ServerClient;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Services
{
    ///<inheritdoc />
    public class InputModelValidationService : IInputModelValidationService
    {
        readonly IDocuWareConnectionService _docuWareConnectionService;
        private readonly IEnumerable<IValueCheck> _valueChecks;
        private readonly IEnumerable<IRootValueCheck> _rootValueChecks;

        /// <summary>
        /// The service validating the inputValueModel.
        /// All method parameters will get injected by DI.
        /// Registration can be found in <see cref="Startup"/>
        /// </summary>
        /// <param name="docuWareConnectionService">Service handling the DocuWare Connection</param>
        /// <param name="valueChecks">Different checks to be done with InputModel Values</param>
        /// <param name="rootValueChecks"></param>
        public InputModelValidationService(IDocuWareConnectionService docuWareConnectionService, IEnumerable<IValueCheck> valueChecks, IEnumerable<IRootValueCheck> rootValueChecks)
        {
            _docuWareConnectionService = docuWareConnectionService;
            _valueChecks = valueChecks;
            _rootValueChecks = rootValueChecks;
        }

        ///<inheritdoc />
        public Task<ReturnValueModel> CheckValuesSimple(InputValueModel inputValueModel)
        {
            List<ValidationResult> valueValidationResults = new List<ValidationResult>();

            foreach (ValueModel valueModel in inputValueModel.Values)
            {
                foreach (IValueCheck validationCheck in _valueChecks)
                {
                    valueValidationResults.Add(validationCheck.ValidateInput(valueModel));
                }
            }

            ValidationResult unsuccessfulValidationResult = valueValidationResults.FirstOrDefault(x => x.Success == false);

            if (unsuccessfulValidationResult == null)
            {
                return Task.FromResult(SuccessReturnModel());
            }

            return Task.FromResult(CustomReturnModel(ReturnModelStatus.Failed,
                unsuccessfulValidationResult.ValidationMessage));
        }

        ///<inheritdoc />
        public Task<ReturnValueModel> CheckRootValuesSimple(InputValueModel inputValueModel)
        {
            List<ValidationResult> valueValidationResults = new List<ValidationResult>();

            foreach (IRootValueCheck validationCheck in _rootValueChecks)
            {
                valueValidationResults.Add(validationCheck.ValidateInput(inputValueModel));
            }

            ValidationResult unsuccessfulValidationResult = valueValidationResults.FirstOrDefault(x => x.Success == false);

            if (unsuccessfulValidationResult == null)
            {
                return Task.FromResult(SuccessReturnModel());
            }

            return Task.FromResult(CustomReturnModel(ReturnModelStatus.Failed,
                unsuccessfulValidationResult.ValidationMessage));
        }

        ///<inheritdoc />
        public Task<ReturnValueModel> CheckValuesAgainstDocuWareSystem(InputValueModel inputValueModel)
        {
            ServiceConnection serviceConnection = _docuWareConnectionService.GetServiceConnection();
            Organization organization = serviceConnection.Organizations.FirstOrDefault();

            if (organization == null)
            {
                return Task.FromResult(CustomReturnModel(ReturnModelStatus.Failed, "Organization is null"));
            }

            Console.WriteLine($"Organization name: {organization.Name}");

            // Do here what ever you want in the DocuWare system!
            // To find inspirations check our developer website:
            // https://developer.docuware.com/dotNet_CodeExamples/d25612d7-49fa-4d66-bfcd-67c5591381f7.html

            return Task.FromResult(SuccessReturnModel("Check against DocuWare system went out fine."));
        }

        ///<inheritdoc />
        public Task<ReturnValueModel> WriteAllValuesToConsole(InputValueModel inputValueModel)
        {
            Console.WriteLine(JsonSerializer.Serialize(inputValueModel, new JsonSerializerOptions { WriteIndented = true }));

            return Task.FromResult(SuccessReturnModel("Write all into console!"));
        }

        private ReturnValueModel SuccessReturnModel(string reason = "All fine")
        {
            return new ReturnValueModel { Status = ReturnModelStatus.Ok, Reason = reason };
        }

        private ReturnValueModel CustomReturnModel(ReturnModelStatus status, string reason)
        {
            return new ReturnValueModel { Status = status, Reason = reason };
        }
    }
}