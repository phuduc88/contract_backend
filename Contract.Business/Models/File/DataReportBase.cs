using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class DataReportBase 
    {
        public UserSessionInfo UserInfo { get; set; }       

        public string FileName { get; set; }

        public string FileTemplates { get; set; }
    }
}
