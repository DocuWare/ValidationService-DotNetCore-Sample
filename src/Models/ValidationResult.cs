namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// Result returned to DocuWare after validation
    /// </summary>
    public class ValidationResult
    {
        public bool Success{ get; set; }
        public string ValidationMessage { get; set; }
    }
}