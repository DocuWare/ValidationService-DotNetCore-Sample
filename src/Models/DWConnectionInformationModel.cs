namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// Represents the connection information model in appSettings.json
    /// </summary>
    public class DwConnectionInformationModel
    {
        public const string Position = "DWConnectionInformation";

        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
