using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMS.Models;
using PMS.DBO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using PMS.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserDetails _Dtls;

        public HomeController(ILogger<HomeController> logger, IOptions<UserDetails> Dtls)
        {
            _logger = logger;
            _Dtls = Dtls.Value;
        }

        public async Task<IActionResult> Index()
        {
            PMSHelper PMShelper = new PMSHelper();
            viewmodel objviewmodel = new viewmodel();

            string userid = string.Empty;
            try
            {
                userid = HttpContext.Session.GetString("UserID");
            }
            catch
            {
                userid = string.Empty;
            }

            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginID", userid);

                objviewmodel.userDtls = await PMShelper.GetUserDtlsByID(ht);
                return View(objviewmodel);
            }
            else
            {
                return View("Login", objviewmodel);
            }
        }

        public async Task<UserDtls> GetUserDtls()
        {
            PMSHelper PMShelper = new PMSHelper();
            UserDtls objUserDtls = new UserDtls();

            string userid = string.Empty;
            try
            {
                userid = HttpContext.Session.GetString("UserID");
            }
            catch
            {
                userid = string.Empty;
            }

            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginID", userid);

                objUserDtls = await PMShelper.GetUserDtlsByID(ht);
                return objUserDtls;
            }
            return objUserDtls;
        }

        public IActionResult Login()
        {
            string userid = string.Empty;
            try
            {
                userid = HttpContext.Session.GetString("UserID");
            }
            catch
            {
                userid = string.Empty;
            }

            if (userid != "" && userid != null)
            {
                return View("Home");
            }
            else
            {
                PMSHelper PMShelper = new PMSHelper();
                viewmodel objviewmodel = new viewmodel();
                return View(objviewmodel);
                
            }

            
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(UserDtls userdtls)
        {
            UserDtls objuserdtls = new UserDtls();
            string result = string.Empty;

            PMSHelper PMShelper = new PMSHelper();
            Hashtable ht = new Hashtable();
            ht.Add("Email", userdtls.EmailID);
            ht.Add("Password", userdtls.Password);
            objuserdtls = await PMShelper.Authenticate(ht);

            
            if (objuserdtls.LoginID != 0)
            {
                string name = objuserdtls.FirstName + " " + objuserdtls.Lastname;
                if (objuserdtls.Adminlndicator == "true")
                {
                    HttpContext.Session.SetString("UserID", Convert.ToString(objuserdtls.LoginID));
                    HttpContext.Session.SetString("UserName", name);
                    
                    var jsonresult = new { Result = "Success", url = Url.Action("dashboard", "admin") };
                    return Json(jsonresult);
                }
                else
                {
                    HttpContext.Session.SetString("UserID", Convert.ToString(objuserdtls.LoginID));
                    HttpContext.Session.SetString("UserName", name);
                    var jsonresult = new { Result = "Success", url = Url.Action("Index") };
                    return Json(jsonresult);
                }

                
            }
            else
            {
                return Content("No data Found");
            }
            return Content(JsonConvert.SerializeObject(objuserdtls));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InstUserDtls(UserDtls userdtls)
        {
            try
            {
                UserDtls objuserdtls = new UserDtls();
                string result = string.Empty;

                PMSHelper PMShelper = new PMSHelper();
                Hashtable ht = new Hashtable();
                ht.Add("UserID", userdtls.LoginID);
                ht.Add("FirstName", userdtls.FirstName);
                ht.Add("LastName", userdtls.Lastname);
                ht.Add("Gender", userdtls.Gender);
                ht.Add("Phone", userdtls.Phone);
                ht.Add("Email", userdtls.EmailID);
                ht.Add("DOB", Convert.ToDateTime(userdtls.DOB));
                ht.Add("State", userdtls.State);
                ht.Add("City", userdtls.City);
                ht.Add("Country", userdtls.Country);
                ht.Add("Pincode", userdtls.Pincode);
                ht.Add("Address", userdtls.Address);
                ht.Add("Password", userdtls.Password);
                ht.Add("ValidUser", true);
                ht.Add("AdminFlag", false);
                result = await PMShelper.InsertUserDtls(ht);

                if (result == "Success")
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserDtls(UserDtls userdtls)
        {
            try
            {
                UserDtls objuserdtls = new UserDtls();
                string result = string.Empty;

                userdtls.LoginID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

                PMSHelper PMShelper = new PMSHelper();
                Hashtable ht = new Hashtable();
                ht.Add("UserID", userdtls.LoginID);
                ht.Add("FirstName", userdtls.FirstName);
                ht.Add("LastName", userdtls.Lastname);
                ht.Add("Gender", userdtls.Gender);
                ht.Add("Phone", userdtls.Phone);
                ht.Add("Email", userdtls.EmailID);
                ht.Add("DOB", Convert.ToDateTime(userdtls.DOB));
                ht.Add("State", userdtls.State);
                ht.Add("City", userdtls.City);
                ht.Add("Country", userdtls.Country);
                ht.Add("Pincode", userdtls.Pincode);
                ht.Add("Address", userdtls.Address);
               
                result = await PMShelper.UpdateUserDtls(ht);

                if (result == "Success")
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ActionResult> UpdatePassword(string oldpass,string newpass,string conpass)
        {
            string result = string.Empty;
            try
            {
                if (newpass!= conpass)
                {
                    result = "New Password Confirm Password Does Not Match";
                }
                else
                {
                    int LoginID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

                    PMSHelper PMShelper = new PMSHelper();
                    Hashtable ht = new Hashtable();
                    ht.Add("UserID", LoginID);
                    ht.Add("OldPass", oldpass);
                    ht.Add("NewPass", newpass);
                    result = await PMShelper.UpdatePassword(ht);

                    if (result == "Success")
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);

        }


        #region Stocks
        public async Task<IActionResult> Stock()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                AdminHelper Adminhelper = new AdminHelper();
                PMSHelper PMShelper = new PMSHelper();
                viewmodel objviewmodel = new viewmodel();

                string stockid = "0";
                string userid = HttpContext.Session.GetString("UserID");

                if (userid != "" && userid != null)
                {

                    Hashtable ht = new Hashtable();
                    ht.Add("LoginID", userid);
                    ht.Add("stockid", stockid);

                    objviewmodel = await PMShelper.GetUserStock(ht);

                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<string> AddStockuser(PMS_UserStockMap stockdata)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {
                
                PMSHelper PMShelper = new PMSHelper();

                stockdata.LoginID = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                stockdata.PurchaseDate = System.DateTime.Now;


                if(stockdata!=null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", stockdata.TransactionNo);
                    ht.Add("LoginID", stockdata.LoginID);
                    ht.Add("StockID", stockdata.StockID);
                    ht.Add("Date", stockdata.PurchaseDate);
                    ht.Add("Qty", stockdata.Quantity);
                    ht.Add("Price", stockdata.PurchasePrice);
                    ht.Add("stockName", stockdata.StockName);
                    result = await PMShelper.AddStockuser(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> UpdateStockuser(PMS_UserStockMap stockdata)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {

                PMSHelper PMShelper = new PMSHelper();

                stockdata.LoginID = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                stockdata.PurchaseDate = System.DateTime.Now;


                if (stockdata != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", stockdata.TransactionNo);
                    ht.Add("LoginID", stockdata.LoginID);
                    ht.Add("StockID", stockdata.StockID);
                    ht.Add("Date", stockdata.PurchaseDate);
                    ht.Add("Qty", stockdata.Quantity);
                    ht.Add("Price", stockdata.PurchasePrice);
                    ht.Add("stockName", stockdata.StockName);
                    result = await PMShelper.UpdateUserStock(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> DeleteStock(PMS_UserStockMap data)
        {
            PMSHelper PMShelper = new PMSHelper();

            string result = string.Empty;
            string userid = HttpContext.Session.GetString("UserID").ToString();
            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("userid", userid);
                ht.Add("transid", data.TransactionNo);
                ht.Add("stockid", data.StockID);

                result = await PMShelper.DeleteStock(ht);
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region MutualFund
        public async Task<IActionResult> MutualFund()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                AdminHelper Adminhelper = new AdminHelper();
                PMSHelper PMShelper = new PMSHelper();
                viewmodel objviewmodel = new viewmodel();

                string MFID = "0";
                string userid = HttpContext.Session.GetString("UserID");

                if (userid != "" && userid != null)
                {

                    Hashtable ht = new Hashtable();
                    ht.Add("LoginID", userid);
                    ht.Add("MFID", MFID);

                    objviewmodel = await PMShelper.GetUserMF(ht);

                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<string> AddMFuser(PMS_UserMFMap MFData)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {
                var chars = "A0B1C2D3E4F5G6H7I8J9KLMNOPQRSTUVWXYZ";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var folio = new String(stringChars);

                PMSHelper PMShelper = new PMSHelper();

                MFData.LoginId = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                MFData.PurchaseDate = System.DateTime.Now;

                if (MFData != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", MFData.MFTransactionNo);
                    ht.Add("MFID", MFData.MFID);
                    ht.Add("LoginID", MFData.LoginId);
                    ht.Add("Date", MFData.PurchaseDate);
                    ht.Add("Qty", MFData.Quantity);
                    ht.Add("PurQty", MFData.PurchaseQty);
                    ht.Add("MFName", MFData.MFName);
                    ht.Add("Folio", folio);
                    result = await PMShelper.AddMFuser(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> UpdateMFuser(PMS_UserMFMap MFData)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {

                PMSHelper PMShelper = new PMSHelper();

                MFData.LoginId = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                MFData.PurchaseDate = System.DateTime.Now;


                if (MFData != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", MFData.MFTransactionNo);
                    ht.Add("LoginID", MFData.LoginId);
                    ht.Add("MFID", MFData.MFID);
                    ht.Add("Date", MFData.PurchaseDate);
                    ht.Add("Qty", MFData.Quantity);
                    ht.Add("PurQty", MFData.PurchaseQty);
                    result = await PMShelper.UpdateUserMF(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> DeleteMF(PMS_UserMFMap MFData)
        {
            PMSHelper PMShelper = new PMSHelper();

            string result = string.Empty;
            string userid = HttpContext.Session.GetString("UserID").ToString();
            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("userid", userid);
                ht.Add("transid", MFData.MFTransactionNo);
                ht.Add("MFID", MFData.MFID);

                result = await PMShelper.DeleteMF(ht);
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region FixedIncome
        public async Task<IActionResult> FixedIncome()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                AdminHelper Adminhelper = new AdminHelper();
                PMSHelper PMShelper = new PMSHelper();
                viewmodel objviewmodel = new viewmodel();

                string FIID = "0";
                string userid = HttpContext.Session.GetString("UserID");

                if (userid != "" && userid != null)
                {

                    Hashtable ht = new Hashtable();
                    ht.Add("LoginID", userid);
                    ht.Add("FIID", FIID);

                    objviewmodel = await PMShelper.GetFixedIncome(ht);

                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<string> AddFIuser(PMS_UserFixedMap FIData)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {
                PMSHelper PMShelper = new PMSHelper();

                FIData.LoginId = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                FIData.PurchaseDate = System.DateTime.Now;

                if (FIData != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", FIData.FITransactionNo);
                    ht.Add("FIID", FIData.FIID);
                    ht.Add("FIName", FIData.FIName);
                    ht.Add("Date", FIData.PurchaseDate);
                    ht.Add("Qty", FIData.PurchaseQty);
                    ht.Add("LoginID", FIData.LoginId);
                    result = await PMShelper.AddFIuser(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> UpdateFIuser(PMS_UserFixedMap FIData)
        {
            string result = "Failure";
            if (HttpContext.Session.GetString("UserID") != null)
            {

                PMSHelper PMShelper = new PMSHelper();

                FIData.LoginId = Convert.ToInt32(HttpContext.Session.GetString("UserID").ToString());
                FIData.PurchaseDate = System.DateTime.Now;


                if (FIData != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TransactionNo", FIData.FITransactionNo);
                    ht.Add("FIID", FIData.FIID);
                    ht.Add("FIName", FIData.FIName);
                    ht.Add("Date", FIData.PurchaseDate);
                    ht.Add("Qty", FIData.PurchaseQty);
                    ht.Add("LoginID", FIData.LoginId);
                    result = await PMShelper.UpdateUserFI(ht);
                }
                return result;
            }
            else
            {
                return result;
            }
        }
        public async Task<string> DeleteFI(PMS_UserFixedMap data)
        {
            PMSHelper PMShelper = new PMSHelper();

            string result = string.Empty;
            string userid = HttpContext.Session.GetString("UserID").ToString();
            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("userid", userid);
                ht.Add("transid", data.FITransactionNo);
                ht.Add("FIID", data.FIID);

                result = await PMShelper.DeleteFI(ht);
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Clear();

            PMSHelper PMShelper = new PMSHelper();
            viewmodel objviewmodel = new viewmodel();

            string userid = string.Empty;
            try
            {
                userid = HttpContext.Session.GetString("UserID");
            }
            catch
            {
                userid = string.Empty;
            }

            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginID", userid);

                objviewmodel.userDtls = await PMShelper.GetUserDtlsByID(ht);
                return View(objviewmodel);
            }
            else
            {
                return View("Login", objviewmodel);
            }

            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
