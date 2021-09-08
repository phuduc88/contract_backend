using System.Linq;
using Contract.Business.Constants;
using Newtonsoft.Json;
namespace Contract.Business.Models
{
    public class FunctionInfo
    {
        private const int UnChecked = 0;
        private const int Checked = 1;
        private const int DisableAction = 2;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("screenName")]
        public string ScreenName { get; set; }

        [JsonProperty("read")]
        public int Read { get; set; }

        [JsonProperty("update")]
        public int Update { get; set; }

        [JsonProperty("create")]
        public int Create { get; set; }

        [JsonProperty("delete")]
        public int Delete { get; set; }

        [JsonProperty("active")]
        public int Active { get; set; }

        [JsonProperty("approve")]
        public int Approve { get; set; }

        [JsonProperty("sign")]
        public int Sign { get; set; }

        [JsonProperty("rejected")]
        public int Rejected { get; set; }

        public FunctionInfo()
        {

        }
        public FunctionInfo(string screenName, int id, char[] action)
        {
            this.ScreenName = screenName;
            this.Id = id;
            this.Read = action.Contains(CharacterAction.Read) ? Checked : DisableAction;
            this.Update = action.Contains(CharacterAction.Update) ? Checked : DisableAction;
            this.Create = action.Contains(CharacterAction.Create) ? Checked : DisableAction;
            this.Delete = action.Contains(CharacterAction.Delete) ? Checked : DisableAction;
            this.Active = action.Contains(CharacterAction.Active) ? Checked : DisableAction;
            this.Approve = action.Contains(CharacterAction.Approve) ? Checked : DisableAction;
            this.Sign = action.Contains(CharacterAction.Sign) ? Checked : DisableAction;
            this.Rejected = action.Contains(CharacterAction.Rejected) ? Checked : DisableAction;
        }

        public void SetValue(char[] action)
        {
            if (this.Approve != DisableAction)
            {
                this.Approve = action.Contains(CharacterAction.Approve) ? Checked : UnChecked;
            }

            if (this.Read != DisableAction)
            {
                this.Read = action.Contains(CharacterAction.Read) ? Checked : UnChecked;
            }

            if (this.Update != DisableAction)
            {
                this.Update = action.Contains(CharacterAction.Update) ? Checked : UnChecked;
            }

            if (this.Delete != DisableAction)
            {
                this.Delete = action.Contains(CharacterAction.Delete) ? Checked : UnChecked;
            }

            if (this.Create != DisableAction)
            {
                this.Create = action.Contains(CharacterAction.Create) ? Checked : UnChecked;
            }

            if (this.Active != DisableAction)
            {
                this.Active = action.Contains(CharacterAction.Active) ? Checked : UnChecked;
            }
            if (this.Rejected != DisableAction)
            {
                this.Rejected = action.Contains(CharacterAction.Rejected) ? Checked : UnChecked;
            }

            if (this.Sign != DisableAction)
            {
                this.Sign = action.Contains(CharacterAction.Sign) ? Checked : UnChecked;
            }
        }

        public string GetAction()
        {
            string action = string.Empty;
            if(this.Read == Checked) {
                action = action + CharacterAction.Read;
            }
            if (this.Update == Checked)
            {
                action = action + CharacterAction.Update;
            }

            if (this.Create == Checked)
            {
                action = action + CharacterAction.Create;
            }

            if (this.Delete == Checked)
            {
                action = action + CharacterAction.Delete;
            }

            if (this.Active == Checked)
            {
                action = action + CharacterAction.Active;
            }

            if (this.Approve == Checked)
            {
                action = action + CharacterAction.Approve;
            }

            if (this.Rejected == Checked)
            {
                action = action + CharacterAction.Rejected;
            }

            if (this.Sign == Checked)
            {
                action = action + CharacterAction.Sign;
            }

            return action;
        }

    }
}
