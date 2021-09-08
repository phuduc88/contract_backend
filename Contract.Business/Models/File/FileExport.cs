using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class FileExport
    {
        [JsonProperty("fullPathFileName")]
        public string FullPathFileName { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fullPathFileInvoice")]
        public string FullPathFileInvoice { get; set; }
        public FileExport()
        {
        }
        public FileExport(string fullPathFileName)
            : this()
        {
            this.FullPathFileName = fullPathFileName;
        }
    }
}
