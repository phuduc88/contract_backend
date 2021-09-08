using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.ExternalData
{
    public class RestApiService
    {
        private RestClient _restClient;
        private static string ApiBaseUrl = "https://thongtindoanhnghiep.co/api/";
        const string XAuthenTokenHeader = "X-Authorization-Token";
        public RestApiService(string apiBaseUrl)
        {
            ApiBaseUrl = apiBaseUrl;
            _restClient = new RestClient(ApiBaseUrl);
        }

         public RestApiService(string authenToken,string apiBaseUrl)
            : this(apiBaseUrl)
        {
            if (_restClient != null)
            {
                _restClient.DefaultRequestHeaders.Add(XAuthenTokenHeader, authenToken);
            }
        }

        // public ExportFileInfo GetInvoiceResearch(string uriapiInfo)
        //{
        //    var response = this._restClient.Get<ApiResponse<ExportFileInfo>>(uriapiInfo);
        //    return response.Data;
        //}
    }
}
