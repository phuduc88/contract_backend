namespace Contract.Business.Models
{
    public class EmailConfig
    {
        public string FolderEmailTemplate { get; set; }
        public bool EnableSsl { get; set; }

        public int TypeEmail { get; set; }
        
    }
}
