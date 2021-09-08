using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ExternalCustomer
    {

        [JsonProperty("MaSoThue")]
        public string MaSoThue { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("TitleEn")]
        public string TitleEn { get; set; }

        [JsonProperty("DiaChiCongTy")]
        public string DiaChiCongTy { get; set; }

        [JsonProperty("GiamDoc")]
        public string GiamDoc { get; set; }

        [JsonProperty("NoiNopThue_DienThoai")]
        public string NoiNopThue_DienThoai { get; set; }

        [JsonProperty("NoiNopThue_Fax")]
        public string NoiNopThue_Fax { get; set; }

        public ExternalCustomer()
        {

        }

    }
}
