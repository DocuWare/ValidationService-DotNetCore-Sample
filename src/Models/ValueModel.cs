using System.Text.Json;

namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// Represents a DocuWare Index Value
    /// </summary>
    public class ValueModel
    {
        public string FieldName { get; set; }
        public string ItemElementName { get; set; }
        public object Item { get; set; }

        public override string ToString()
        {
            return string.Format("FieldName {1}{0}" +
                                 "ItemElementName: {2}{0}" +
                                 "Item: {3}{0}",
                System.Environment.NewLine,
                FieldName,
                ItemElementName,
                JsonSerializer.Serialize(Item));
        }
    }
}