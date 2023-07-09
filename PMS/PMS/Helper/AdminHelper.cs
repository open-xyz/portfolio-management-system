using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMS.Models;
using System.Collections;
using System.Text;

namespace PMS.Helper
{
    public class AdminHelper : Controller
    {
        private static string ApiDomain;

        static AdminHelper()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

            ApiDomain = config.GetSection("ApiDomain").Value;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        public async Task<string> InsertUserDtls(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "InsertUserDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<string> SaveStockDtls(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "SaveStockDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<string> SaveMFDtls(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "SaveMFDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<string> SaveFIDtls(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "SaveFIDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<PMS_MutualFundMaster> GetMFByID(Hashtable ht)
        {
            PMS_MutualFundMaster objMFdtls = new PMS_MutualFundMaster();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetMFByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objMFdtls = JsonConvert.DeserializeObject<PMS_MutualFundMaster>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objMFdtls;

        }

        public async Task<PMS_FixedIncomeMaster> GetFIByID(Hashtable ht)
        {
            PMS_FixedIncomeMaster objFIdtls = new PMS_FixedIncomeMaster();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetFIByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objFIdtls = JsonConvert.DeserializeObject<PMS_FixedIncomeMaster>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objFIdtls;

        }

        public async Task<string> DeleteMFByID(Hashtable ht)
        {
            UserDtls objuserdtls = new UserDtls();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "DeleteMFByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<string> DeleteFIByID(Hashtable ht)
        {
            string strResponse = string.Empty;
            var Response = await ExecuteService("PMS", "DeleteFIByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<string> getlistcount()
        {
            string strResponse = string.Empty;
            var Response = await ExecuteService("PMS", "getlistcount", null);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<List<UserDtls>> SearchByID(Hashtable ht)
        {
            List<UserDtls> objlstuserdtls = new List<UserDtls>();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "SearchByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstuserdtls = JsonConvert.DeserializeObject<List<UserDtls>>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objlstuserdtls;

        }

        public async Task<List<UserDtls>> GetUserDtls(Hashtable ht)
        {
            List<UserDtls> objlstuserdtls = new List<UserDtls>();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetUserDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstuserdtls = JsonConvert.DeserializeObject<List<UserDtls>>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objlstuserdtls;

        }

        public async Task<viewmodel> GetStockDtls(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstStockDtls objlststockdetails = new lstStockDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetStockDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlststockdetails.listStockDtls = JsonConvert.DeserializeObject<List<PMS_StockMaster>>(strResponse);
                objviewmodel.lststockDtls=objlststockdetails;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;

        }

        public async Task<viewmodel> GetMutualFund(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstMFDtls objlstMFdetails = new lstMFDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetMutualFund", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstMFdetails.listMFDtls = JsonConvert.DeserializeObject<List<PMS_MutualFundMaster>>(strResponse);
                objviewmodel.lstMFDtls = objlstMFdetails;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;

        }

        public async Task<viewmodel> GetFixeIncome(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstFIDtls objlstFIdetails = new lstFIDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetFixedIncome", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstFIdetails.listFIDtls = JsonConvert.DeserializeObject<List<PMS_FixedIncomeMaster>>(strResponse);
                objviewmodel.lstFIDtls = objlstFIdetails;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;

        }


        public async Task<string> DeleteUserDtlsByID(Hashtable ht)
        {
            UserDtls objuserdtls = new UserDtls();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "DeleteUserDtlsByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return strResponse;

        }

        public async Task<HttpResponseMessage> ExecuteService(string controller, string action, System.Object data)
        {
            try
            {
                string Baseurl = ApiDomain + "/" + controller + "/" + action;

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) => true;

                    using (var client = new HttpClient(httpClientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        return await client.PostAsync(Baseurl, content);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
