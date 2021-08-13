namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// The cookie model returned by DocuWare (or in general)
    /// </summary>
    public class DwCookieModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public string Domain { get; set; }
    }
}