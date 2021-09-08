using Contract.Data.DBAccessor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contract.Business.Helper
{
    /// <summary>
    ///     Class Mail builds an object that sends an email through SendGrid.
    /// </summary>
    public class Mail
    {
        private Email from;
        private String subject;
        private List<Personalization> personalizations;
        private List<Content> contents;
        private List<Attachment> attachments;
        private String templateId;
        private Dictionary<String, String> headers;
        private Dictionary<String, String> sections;
        private List<String> categories;
        private Dictionary<String, String> customArgs;
        private long? sendAt;
        private ASM asm;
        private String batchId;
        private String setIpPoolId;
        private MailSettings mailSettings;
        private TrackingSettings trackingSettings;
        private Email replyTo;

        public Mail()
        {
            return;
        }

        public Mail(Email from, String subject, Email emailTo, Content content)
        {
            this.From = from;
            Personalization personalization = SetPersonalization(emailTo);
            this.AddPersonalization(personalization);
            this.Subject = subject;
            this.AddContent(content);
        }

        private Helper.Personalization SetPersonalization(Email emailTo)
        {
            Personalization personalization = new Personalization();
            if (emailTo.EmailBccs == null || emailTo.EmailBccs.Count == 0)
            {
                personalization.AddTo(emailTo);
            }
            else
            {
                personalization.AddTo(this.From);
                personalization.Bccs = emailTo.EmailBccs.Select(p => new Email(p)).ToList();
            }

            return personalization;
        }

        [JsonProperty("from")]
        public Email From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        [JsonProperty("subject")]
        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        [JsonProperty("personalizations")]
        public List<Personalization> Personalization
        {
            get
            {
                return personalizations;
            }

            set
            {
                personalizations = value;
            }
        }

        [JsonProperty("content")]
        public List<Content> Contents
        {
            get
            {
                return contents;
            }

            set
            {
                contents = value;
            }
        }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments
        {
            get
            {
                return attachments;
            }

            set
            {
                attachments = value;
            }
        }

        [JsonProperty("template_id")]
        public string TemplateId
        {
            get
            {
                return templateId;
            }

            set
            {
                templateId = value;
            }
        }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers
        {
            get
            {
                return headers;
            }

            set
            {
                headers = value;
            }
        }

        [JsonProperty("sections")]
        public Dictionary<string, string> Sections
        {
            get
            {
                return sections;
            }

            set
            {
                sections = value;
            }
        }

        [JsonProperty("categories")]
        public List<string> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
            }
        }

        [JsonProperty("custom_args")]
        public Dictionary<string, string> CustomArgs
        {
            get
            {
                return customArgs;
            }

            set
            {
                customArgs = value;
            }
        }

        [JsonProperty("send_at")]
        public long? SendAt
        {
            get
            {
                return sendAt;
            }

            set
            {
                sendAt = value;
            }
        }

        [JsonProperty("asm")]
        public ASM Asm
        {
            get
            {
                return asm;
            }

            set
            {
                asm = value;
            }
        }

        [JsonProperty("batch_id")]
        public string BatchId
        {
            get
            {
                return batchId;
            }

            set
            {
                batchId = value;
            }
        }

        [JsonProperty("ip_pool_name")]
        public string SetIpPoolId
        {
            get
            {
                return setIpPoolId;
            }

            set
            {
                setIpPoolId = value;
            }
        }

        [JsonProperty("mail_settings")]
        public MailSettings MailSettings
        {
            get
            {
                return mailSettings;
            }

            set
            {
                mailSettings = value;
            }
        }

        [JsonProperty("tracking_settings")]
        public TrackingSettings TrackingSettings
        {
            get
            {
                return trackingSettings;
            }

            set
            {
                trackingSettings = value;
            }
        }

        [JsonProperty("reply_to")]
        public Email ReplyTo
        {
            get
            {
                return replyTo;
            }

            set
            {
                replyTo = value;
            }
        }

        public void AddPersonalization(Personalization personalization)
        {
            if (Personalization == null)
            {
                Personalization = new List<Personalization>();
            }
            Personalization.Add(personalization);
        }

        public void AddContent(Content content)
        {
            if (Contents == null)
            {
                Contents = new List<Content>();
            }
            Contents.Add(content);
        }

        public void AddAttachment(Attachment attachment)
        {
            if (Attachments == null)
            {
                Attachments = new List<Attachment>();
            }
            Attachments.Add(attachment);
        }

        public void AddHeader(String key, String value)
        {
            if (headers == null)
            {
                headers = new Dictionary<String, String>();
            }
            headers.Add(key, value);
        }

        public void AddSection(String key, String value)
        {
            if (sections == null)
            {
                sections = new Dictionary<String, String>();
            }
            sections.Add(key, value);
        }

        public void AddCategory(String category)
        {
            if (Categories == null)
            {
                Categories = new List<String>();
            }
            Categories.Add(category);
        }

        public void AddCustomArgs(String key, String value)
        {
            if (customArgs == null)
            {
                customArgs = new Dictionary<String, String>();
            }
            customArgs.Add(key, value);
        }

        public String Get()
        {
            return JsonConvert.SerializeObject(this,
                                Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include, StringEscapeHandling = StringEscapeHandling.EscapeHtml });
        }
    }

    public class ClickTracking
    {
        private bool? enable;
        private bool? enableText;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("enable_text")]
        public bool? EnableText
        {
            get
            {
                return enableText;
            }

            set
            {
                enableText = value;
            }
        }
    }

    public class OpenTracking
    {
        private bool? enable;
        private String substitutionTag;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("substitution_tag")]
        public string SubstitutionTag
        {
            get
            {
                return substitutionTag;
            }

            set
            {
                substitutionTag = value;
            }
        }
    }

    public class SubscriptionTracking
    {
        private bool? enable;
        private String text;
        private String html;
        private String substitutionTag;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("text")]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        [JsonProperty("html")]
        public string Html
        {
            get
            {
                return html;
            }

            set
            {
                html = value;
            }
        }

        [JsonProperty("substitution_tag")]
        public string SubstitutionTag
        {
            get
            {
                return substitutionTag;
            }

            set
            {
                substitutionTag = value;
            }
        }
    }

    public class Ganalytics
    {
        private bool? enable;
        private String utmSource;
        private String utmMedium;
        private String utmTerm;
        private String utmContent;
        private String utmCampaign;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("utm_source")]
        public string UtmSource
        {
            get
            {
                return utmSource;
            }

            set
            {
                utmSource = value;
            }
        }

        [JsonProperty("utm_medium")]
        public string UtmMedium
        {
            get
            {
                return utmMedium;
            }

            set
            {
                utmMedium = value;
            }
        }

        [JsonProperty("utm_term")]
        public string UtmTerm
        {
            get
            {
                return utmTerm;
            }

            set
            {
                utmTerm = value;
            }
        }

        [JsonProperty("utm_content")]
        public string UtmContent
        {
            get
            {
                return utmContent;
            }

            set
            {
                utmContent = value;
            }
        }

        [JsonProperty("utm_campaign")]
        public string UtmCampaign
        {
            get
            {
                return utmCampaign;
            }

            set
            {
                utmCampaign = value;
            }
        }
    }

    public class TrackingSettings
    {
        private ClickTracking clickTracking;
        private OpenTracking openTracking;
        private SubscriptionTracking subscriptionTracking;
        private Ganalytics ganalytics;

        [JsonProperty("click_tracking")]
        public ClickTracking ClickTracking
        {
            get
            {
                return clickTracking;
            }

            set
            {
                clickTracking = value;
            }
        }

        [JsonProperty("open_tracking")]
        public OpenTracking OpenTracking
        {
            get
            {
                return openTracking;
            }

            set
            {
                openTracking = value;
            }
        }

        [JsonProperty("subscription_tracking")]
        public SubscriptionTracking SubscriptionTracking
        {
            get
            {
                return subscriptionTracking;
            }

            set
            {
                subscriptionTracking = value;
            }
        }

        [JsonProperty("ganalytics")]
        public Ganalytics Ganalytics
        {
            get
            {
                return ganalytics;
            }

            set
            {
                ganalytics = value;
            }
        }
    }

    public class BCCSettings
    {
        private bool? enable;
        private String email;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("email")]
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
    }

    public class BypassListManagement
    {
        private bool? enable;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
    }

    public class FooterSettings
    {
        private bool? enable;
        private String text;
        private String html;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("text")]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        [JsonProperty("html")]
        public string Html
        {
            get
            {
                return html;
            }

            set
            {
                html = value;
            }
        }
    }

    public class SandboxMode
    {
        private bool? enable;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
    }

    public class SpamCheck
    {
        private bool? enable;
        private int? threshold;
        private String postToUrl;

        [JsonProperty("enable")]
        public bool? Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty("threshold")]
        public int? Threshold
        {
            get
            {
                return threshold;
            }

            set
            {
                threshold = value;
            }
        }

        [JsonProperty("post_to_url")]
        public string PostToUrl
        {
            get
            {
                return postToUrl;
            }

            set
            {
                postToUrl = value;
            }
        }
    }

    public class MailSettings
    {
        private BCCSettings bccSettings;
        private BypassListManagement bypassListManagement;
        private FooterSettings footerSettings;
        private SandboxMode sandboxMode;
        private SpamCheck spamCheck;

        [JsonProperty("bcc")]
        public BCCSettings BccSettings
        {
            get
            {
                return bccSettings;
            }

            set
            {
                bccSettings = value;
            }
        }

        [JsonProperty("bypass_list_management")]
        public BypassListManagement BypassListManagement
        {
            get
            {
                return bypassListManagement;
            }

            set
            {
                bypassListManagement = value;
            }
        }

        [JsonProperty("footer")]
        public FooterSettings FooterSettings
        {
            get
            {
                return footerSettings;
            }

            set
            {
                footerSettings = value;
            }
        }

        [JsonProperty("sandbox_mode")]
        public SandboxMode SandboxMode
        {
            get
            {
                return sandboxMode;
            }

            set
            {
                sandboxMode = value;
            }
        }

        [JsonProperty("spam_check")]
        public SpamCheck SpamCheck
        {
            get
            {
                return spamCheck;
            }

            set
            {
                spamCheck = value;
            }
        }
    }

    public class ASM
    {
        private int? groupId;
        private List<int> groupsToDisplay;

        [JsonProperty("group_id")]
        public int? GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
            }
        }

        [JsonProperty("groups_to_display")]
        public List<int> GroupsToDisplay
        {
            get
            {
                return groupsToDisplay;
            }

            set
            {
                groupsToDisplay = value;
            }
        }
    }

    public class Attachment
    {
        private String content;
        private String type;
        private String filename;
        private String disposition;
        private String contentId;

        [JsonProperty("content")]
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        [JsonProperty("filename")]
        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        [JsonProperty("disposition")]
        public string Disposition
        {
            get
            {
                return disposition;
            }

            set
            {
                disposition = value;
            }
        }

        [JsonProperty("content_id")]
        public string ContentId
        {
            get
            {
                return contentId;
            }

            set
            {
                contentId = value;
            }
        }
    }

    public class Content
    {
        private String type;
        private String value;

        public Content()
        {
            return;
        }

        public Content(String type, String value)
        {
            this.Type = type;
            this.Value = value;
        }

        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        [JsonProperty("value")]
        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }

    public class Email
    {
        private String name;
        private String address;
        public List<string> EmailBccs { get; set; }
        public Email()
        {
            return;
        }

        public Email(String email, List<string> emailBcc = null, String name = null)
        {
            this.Address = email;
            this.EmailBccs = emailBcc;
            this.Name = name;
        }

        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [JsonProperty("email")]
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
    }

    public class Personalization
    {
        private List<Email> tos;
        private List<Email> ccs;
        private List<Email> bccs;
        private String subject;
        private Dictionary<String, String> headers;
        private Dictionary<String, String> substitutions;
        private Dictionary<String, String> customArgs;
        private long? sendAt;

        public bool ShouldSerializeTos()
        {
            if (this.tos != null)
            {
                return this.tos.Any();
            }
            return false;
        }

        [JsonProperty("to")]
        public List<Email> Tos
        {
            get
            {
                return tos;
            }

            set
            {
                tos = value;
            }
        }


        public bool ShouldSerializeCcs()
        {
            if (this.ccs != null)
            {
                return this.ccs.Any();
            }
            return false;
        }

        [JsonProperty("cc")]
        public List<Email> Ccs
        {
            get
            {
                return ccs;
            }

            set
            {
                ccs = value;
            }
        }

        public bool ShouldSerializeBccs()
        {
            if (this.bccs != null)
            {
                return this.bccs.Any();
            }
            return false;
        }

        [JsonProperty("bcc")]
        public List<Email> Bccs
        {
            get
            {
                return bccs;
            }

            set
            {
                bccs = value;
            }
        }

        [JsonProperty("subject")]
        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers
        {
            get
            {
                return headers;
            }

            set
            {
                headers = value;
            }
        }

        [JsonProperty("substitutions")]
        public Dictionary<string, string> Substitutions
        {
            get
            {
                return substitutions;
            }

            set
            {
                substitutions = value;
            }
        }

        [JsonProperty("custom_args")]
        public Dictionary<string, string> CustomArgs
        {
            get
            {
                return customArgs;
            }

            set
            {
                customArgs = value;
            }
        }

        [JsonProperty("send_at")]
        public long? SendAt
        {
            get
            {
                return sendAt;
            }

            set
            {
                sendAt = value;
            }
        }

        public void AddTo(Email email)
        {
            if (tos == null)
            {
                tos = new List<Email>();

            }
            tos.Add(email);
        }

        public void AddCc(Email email)
        {
            if (ccs == null)
            {
                ccs = new List<Email>();
            }
            ccs.Add(email);
        }

        public void AddBcc(Email email)
        {
            if (bccs == null)
            {
                bccs = new List<Email>();
            }
            bccs.Add(email);
        }

        public void AddHeader(String key, String value)
        {
            if (headers == null)
            {
                headers = new Dictionary<String, String>();
            }
            headers.Add(key, value);
        }

        public void AddSubstitution(String key, String value)
        {
            if (substitutions == null)
            {
                substitutions = new Dictionary<String, String>();
            }
            substitutions.Add(key, value);
        }

        public void AddCustomArgs(String key, String value)
        {
            if (customArgs == null)
            {
                customArgs = new Dictionary<String, String>();
            }
            customArgs.Add(key, value);
        }
    }

    public class SendGridAPIClient
    {
        public dynamic client;
        public string Version;

        private enum Methods
        {
            GET, POST, PATCH, DELETE
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
         public SendGridAPIClient(string apiKey, string baseUri, string version)
        {
            //_baseUri = new Uri(baseUri);
            //_apiKey = apiKey;
            //Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //Dictionary<String, String> requestHeaders = new Dictionary<String, String>();
            //requestHeaders.Add("Authorization", "Bearer " + apiKey);
            //requestHeaders.Add("Content-Type", "application/json");
            ////requestHeaders.Add("User-Agent", "sendgrid/" + Version + " csharp");
            //requestHeaders.Add("Accept", "application/json");
            //client = new Client(host: baseUri, requestHeaders: requestHeaders, version: version);
        }
    }
}
