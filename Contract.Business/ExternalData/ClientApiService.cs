using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.ExternalData
{
    public class ClientApiService
    {
        private RestClient _restClient;
        const string ApiGetCompany = "company/{0}";
        const string ApiBaseUrl = "https://thongtindoanhnghiep.co/api/";
        public ClientApiService()
        {
            _restClient = new RestClient(ApiBaseUrl);
       }
        
    }
}
