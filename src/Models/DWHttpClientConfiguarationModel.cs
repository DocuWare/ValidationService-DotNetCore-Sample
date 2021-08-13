using System.Collections.Generic;

namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// Represents the configuration value in appSettings.json
    /// </summary>
    public class DwHttpClientConfigurationModel
    {
        public const string Position = "DWHttpClientConfiguration";

        public List<DwCookieModel> Cookies { get; set; }
    }
}
