using System.Text.Json.Serialization;

namespace ValidationServiceDotNetCoreSample.Models
{
    public class ReturnValueModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReturnModelStatus Status { get; set; } = ReturnModelStatus.Unknown;
        public string Reason { get; set; }
    }

    public enum ReturnModelStatus
    {
        Unknown = 0,
        Ok = 1,
        Failed = 2
    }
}