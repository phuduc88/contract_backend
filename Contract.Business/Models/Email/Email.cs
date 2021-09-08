using System;
using System.Collections.Generic;
using Contract.Common.Extensions;
using System.Text;

namespace Contract.Business.Models
{
    public class EmailInfo
    {
        public string Name { get; set; }

        public string EmailTo { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<string> EmailBccs { get; set; }

        public List<File_Info> Files { get; set; }

        public int? CompanyId { get; set; }

        public int? AccountId { get; set; }

        public int? TypeEmail { get; set; }

        public EmailInfo()
        {
            this.Files = new List<File_Info>();
            this.EmailBccs = new List<string>();
        }

        public EmailInfo(string name, string emailto, string subject, string content, List<string> emailBccs)
            : this()
        {
            this.Name = name;
            this.EmailTo = emailto;
            this.Subject = subject;
            this.Content = content;
            this.EmailBccs = emailBccs;
        }
    }
}
