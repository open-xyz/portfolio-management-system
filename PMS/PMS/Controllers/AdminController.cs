using Microsoft.AspNetCore.Mvc;
using PMS.Models;
using PMS.DBO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PMS.Helper;

namespace PMS.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewBag.UserName = "Hi! " + HttpContext.Session.GetString("UserName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }



        public async Task<IActionResult> Stock(IFormCollection collection)

        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewBag.UserName = "Hi! " + HttpContext.Session.GetString("UserName");
                AdminHelper Adminhelper = new AdminHelper();
                viewmodel objviewmodel = new viewmodel();

                string stockid = "0";
                if (collection.Count == 0)
                {
                    stockid = "0";
                }
                else
                {
                    stockid = collection["txtsearch"].ToString();
                }


                if (stockid != "" && stockid != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("stockid", stockid);

                    objviewmodel = await Adminhelper.GetStockDtls(ht);
                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> MutualFund(IFormCollection collection)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewBag.UserName = "Hi! " + HttpContext.Session.GetString("UserName");
                AdminHelper Adminhelper = new AdminHelper();
                viewmodel objviewmodel = new viewmodel();

                string MFID = "0";
                if (collection.Count == 0)
                {
                    MFID = "0";
                }
                else
                {
                    MFID = collection["txtsearch"].ToString();
                }


                if (MFID != "" && MFID != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("MFID", MFID);

                    objviewmodel = await Adminhelper.GetMutualFund(ht);
                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> FixedIncome()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewBag.UserName = "Hi! " + HttpContext.Session.GetString("UserName");
                AdminHelper Adminhelper = new AdminHelper();
                viewmodel objviewmodel = new viewmodel();

                string FIID = "0";

                if (FIID != "" && FIID != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("FIID", FIID);

                    objviewmodel = await Adminhelper.GetFixeIncome(ht);
                }

                return View(objviewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<PMS_MutualFundMaster> GetMFByID(PMS_MutualFundMaster data)
        {
            AdminHelper adminhelper = new AdminHelper();
            PMS_MutualFundMaster objMFdtls = new PMS_MutualFundMaster();

            string mfid = Convert.ToString(data.MFID);
            if (mfid != "" && mfid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("MFID", data.MFID);

                objMFdtls = await adminhelper.GetMFByID(ht);
                return objMFdtls;
            }
            else
            {
                return objMFdtls;
            }
        }

        public async Task<PMS_FixedIncomeMaster> GetFIByID(PMS_FixedIncomeMaster data)
        {
            AdminHelper adminhelper = new AdminHelper();
            PMS_FixedIncomeMaster objMFdtls = new PMS_FixedIncomeMaster();

            string FIID = Convert.ToString(data.FIID);
            if (FIID != "" && FIID != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("FIID", data.FIID);

                objMFdtls = await adminhelper.GetFIByID(ht);
                return objMFdtls;
            }
            else
            {
                return objMFdtls;
            }
        }

        public async Task<string> DeleteMFByID(PMS_MutualFundMaster data)
        {
            AdminHelper Adminhelper = new AdminHelper();
            PMS_MutualFundMaster objMFdtls = new PMS_MutualFundMaster();

            string result = string.Empty;
            string mfid = Convert.ToString(data.MFID);
            if (mfid != "" && mfid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("MFID", data.MFID);

                result = await Adminhelper.DeleteMFByID(ht);
                return result;
            }
            else
            {
                return result;
            }


        }

        public async Task<string> DeleteFIByID(PMS_FixedIncomeMaster data)
        {
            AdminHelper Adminhelper = new AdminHelper();
            PMS_FixedIncomeMaster objFIdtls = new PMS_FixedIncomeMaster();

            string result = string.Empty;
            string FIID = Convert.ToString(data.FIID);
            if (FIID != "" && FIID != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("FIID", data.FIID);

                result = await Adminhelper.DeleteFIByID(ht);
                return result;
            }
            else
            {
                return result;
            }


        }

        public async Task<string> getlistcount()
        {
            AdminHelper Adminhelper = new AdminHelper();

            string result = string.Empty;

            result = await Adminhelper.getlistcount();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> SaveStockDtls(PMS_StockMaster stockdtls)
        {
            try
            {
                UserDtls objuserdtls = new UserDtls();
                string result = string.Empty;

                AdminHelper adminhelper = new AdminHelper();
                Hashtable ht = new Hashtable();
                ht.Add("stockid", "0");
                ht.Add("stockname", stockdtls.StockName);
                ht.Add("facevalue", stockdtls.FaceValue);

                result = await adminhelper.SaveStockDtls(ht);

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
        public async Task<ActionResult> SaveMFDtls(PMS_MutualFundMaster mfdtls)
        {
            try
            {
                UserDtls objuserdtls = new UserDtls();
                string result = string.Empty;

                AdminHelper adminhelper = new AdminHelper();
                Hashtable ht = new Hashtable();
                ht.Add("MFID", "0");
                ht.Add("MFName", mfdtls.MFName);
                ht.Add("MFHouse", mfdtls.FundHouse);
                ht.Add("MFType", mfdtls.FundType);
                ht.Add("FaceValue", mfdtls.FaceValue);

                result = await adminhelper.SaveMFDtls(ht);

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
        public async Task<ActionResult> SaveFIDtls(PMS_FixedIncomeMaster FIdtls)
        {
            try
            {
                UserDtls objuserdtls = new UserDtls();
                string result = string.Empty;

                AdminHelper adminhelper = new AdminHelper();
                Hashtable ht = new Hashtable();
                ht.Add("FIID", "0");
                ht.Add("FIName", FIdtls.FIName);
                ht.Add("FIDesc", FIdtls.FIDescription);
                ht.Add("FIROI", FIdtls.RateOfInterest);
                ht.Add("FITenure", FIdtls.Tenure);
                ht.Add("FIPUV", FIdtls.PurchaseUnitValue);

                result = await adminhelper.SaveFIDtls(ht);

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

        public async Task<IActionResult> Users(IFormCollection collection)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewBag.UserName = "Hi! " + HttpContext.Session.GetString("UserName");
                AdminHelper Adminhelper = new AdminHelper();
                lstUserDtls lstUserdtls = new lstUserDtls();

                string userid = "0";
                if (collection.Count == 0)
                {
                    userid = "0";
                }
                else
                {
                    userid = collection["txtsearch"].ToString();
                }


                if (userid != "" && userid != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("LoginID", userid);

                    lstUserdtls.listUserDtls = await Adminhelper.GetUserDtls(ht);
                }

                return View(lstUserdtls);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        public async Task<UserDtls> GetUserDtlsByID(UserDtls data)
        {
            PMSHelper PMShelper = new PMSHelper();
            UserDtls objuserdtls = new UserDtls();

            string userid = Convert.ToString(data.LoginID);
            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginID", userid);

                objuserdtls = await PMShelper.GetUserDtlsByID(ht);
                return objuserdtls;
            }
            else
            {
                return objuserdtls;
            }
        }

        public async Task<string> DeleteUserDtlsByID(UserDtls data)
        {
            AdminHelper Adminhelper = new AdminHelper();
            UserDtls objuserdtls = new UserDtls();

            string result = string.Empty;
            string userid = Convert.ToString(data.LoginID);
            if (userid != "" && userid != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginID", userid);

                result = await Adminhelper.DeleteUserDtlsByID(ht);
                return result;
            }
            else
            {
                return result;
            }
        }




    }
}
