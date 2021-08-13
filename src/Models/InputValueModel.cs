using System.Collections.Generic;
using System.Text.Json;

namespace ValidationServiceDotNetCoreSample.Models
{
    public class InputValueModel
    {
        public string TimeStamp { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
        public string FileCabinetGuid { get; set; }
        public string DialogGuid { get; set; }
        public string DialogType { get; set; }
        public List<ValueModel> Values { get; set; }

        public override string ToString()
        {
            return string.Format("TimeStamp {1}{0}" +
                                 "UserName: {2}{0}" +
                                 "OrganizationName: {3}{0}" +
                                 "FileCabinetGuid: {4}{0}" +
                                 "DialogGuid: {5}{0}" +
                                 "DialogType: {6}{0}" +
                                 "Values: {0}{7}{0}",
                System.Environment.NewLine,
                TimeStamp,
                UserName,
                OrganizationName,
                FileCabinetGuid,
                DialogGuid,
                DialogType,
                JsonSerializer.Serialize(Values, new JsonSerializerOptions {WriteIndented = true}));
        }
    }
}