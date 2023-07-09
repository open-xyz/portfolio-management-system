using Microsoft.AspNetCore.Mvc;
using PMS.DBO;
using PMS.Models;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Net.Security;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace PMS.Helper
{
    public class PMSHelper : Controller
    {
        private static string ApiDomain;

        static PMSHelper()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

            ApiDomain = config.GetSection("ApiDomain").Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<UserDtls> Authenticate(Hashtable ht)
        {
            UserDtls objuserdtls = new UserDtls();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "Authenticate", ht);
            if(Response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objuserdtls = JsonConvert.DeserializeObject<UserDtls>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objuserdtls;

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

        public async Task<string> UpdateUserDtls(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "UpdateUserDtls", ht);
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

        public async Task<string> UpdatePassword(Hashtable ht)
        {
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "UpdatePassword", ht);
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

        public async Task<UserDtls> GetUserDtlsByID(Hashtable ht)
        {
            UserDtls objuserdtls = new UserDtls();
            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetUserDtlsByID", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objuserdtls = JsonConvert.DeserializeObject<UserDtls>(strResponse);
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objuserdtls;

        }

        #region stocks
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
                objviewmodel.lststockDtls = objlststockdetails;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;

        }
        public async Task<viewmodel> GetUserStock(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstUserStockDtls objlstuserstockdetails = new lstUserStockDtls();
            UserDtls objuserdtls = new UserDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetUserStockDtls", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstuserstockdetails.listUserStockDtls = JsonConvert.DeserializeObject<List<PMS_UserStockMap>>(strResponse);

                objuserdtls = await GetUserDtlsByID(ht);
                objviewmodel = await GetStockDtls(ht);
                objviewmodel.lstuserstockDtls = objlstuserstockdetails;
                objviewmodel.userDtls = objuserdtls;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;
        }
        public async Task<string> AddStockuser(Hashtable ht)
        {
            
            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "AddStockuser", ht);
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
        public async Task<string> UpdateUserStock(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "UpdateUserStock", ht);
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
        public async Task<string> DeleteStock(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "DeleteUserStock", ht);
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
        #endregion

        #region MutualFund
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
        public async Task<viewmodel> GetUserMF(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstUserMFDtls objlstusermfdtls = new lstUserMFDtls();
            UserDtls objuserdtls = new UserDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetUserMF", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstusermfdtls.listUserMFDtls = JsonConvert.DeserializeObject<List<PMS_UserMFMap>>(strResponse);

                objuserdtls = await GetUserDtlsByID(ht);
                objviewmodel = await GetMutualFund(ht);
                objviewmodel.lstUserMFDtls = objlstusermfdtls;
                objviewmodel.userDtls = objuserdtls;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;
        }
        public async Task<string> AddMFuser(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "AddMFuser", ht);
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
        public async Task<string> UpdateUserMF(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "UpdateUserMF", ht);
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
        public async Task<string> DeleteMF(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "DeleteUserMF", ht);
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
        #endregion

        #region FixedIncome
        public async Task<viewmodel> GetFixedIncome(Hashtable ht)
        {
            UserDtls objuserdtls = new UserDtls();
            viewmodel objviewmodel = new viewmodel();
            lstFIDtls objlstFIdetails = new lstFIDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetFixedIncome", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstFIdetails.listFIDtls = JsonConvert.DeserializeObject<List<PMS_FixedIncomeMaster>>(strResponse);
                objuserdtls = await GetUserDtlsByID(ht);
                objviewmodel = await GetUserFI(ht);
                objviewmodel.lstFIDtls = objlstFIdetails;
                objviewmodel.userDtls = objuserdtls;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;

        }
        public async Task<viewmodel> GetUserFI(Hashtable ht)
        {
            viewmodel objviewmodel = new viewmodel();
            lstUserFIDtls objlstuserfidtls = new lstUserFIDtls();
            UserDtls objuserdtls = new UserDtls();

            string strResponse = string.Empty;


            var Response = await ExecuteService("PMS", "GetUserFI", ht);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                strResponse = Response.Content.ReadAsStringAsync().Result;
                objlstuserfidtls.listUserFIDtls = JsonConvert.DeserializeObject<List<PMS_UserFixedMap>>(strResponse);

                objuserdtls = await GetUserDtlsByID(ht);
                objviewmodel.lstUserFIDtls = objlstuserfidtls;
                objviewmodel.userDtls = objuserdtls;
            }
            else
            {
                throw new Exception(Response.Content.ReadAsStringAsync().Result);
            }
            return objviewmodel;
        }
        public async Task<string> AddFIuser(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "AddFIuser", ht);
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
        public async Task<string> UpdateUserFI(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "UpdateUserFI", ht);
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
        public async Task<string> DeleteFI(Hashtable ht)
        {

            string strResponse = string.Empty;

            var Response = await ExecuteService("PMS", "DeleteUserFI", ht);
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
        #endregion

        public async Task<HttpResponseMessage> ExecuteService(string controller,string action, System.Object data)
        {
            try
            {
                string Baseurl = ApiDomain + "/" + controller + "/" + action;

                using(var httpClientHandler=new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) => true;

                    using(var client=new HttpClient(httpClientHandler))
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
