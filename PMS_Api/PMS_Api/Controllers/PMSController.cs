using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS_Api.Models;
using System.Collections;
using System.Linq;

namespace PMS_Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PMSController : ControllerBase
    {
        private readonly PMSContext _dbContext;

        public PMSController(PMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserDtls>>> GetUserDtls(Hashtable ht)
        {
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (LoginID != 0)
            {
                return await _dbContext.PMS_User.Where(i => i.Adminlndicator != true && i.LoginID == LoginID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_User.Where(i => i.Adminlndicator != true).ToListAsync();
            }
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_StockMaster>>> GetStockDtls(Hashtable ht)
        {
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            int stockid = Convert.ToInt32(ht["stockid"].ToString());
            if (stockid != 0)
            {
                return await _dbContext.PMS_StockMaster.Where(i => i.StockID == stockid).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_StockMaster.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_MutualFundMaster>>> GetMutualFund(Hashtable ht)
        {
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            int MFID = Convert.ToInt32(ht["MFID"].ToString());
            if (MFID != 0)
            {
                return await _dbContext.PMS_MutualFundMaster.Where(i => i.MFID == MFID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_MutualFundMaster.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_FixedIncomeMaster>>> GetFixedIncome(Hashtable ht)
        {
            if (_dbContext.PMS_FixedIncomeMaster == null)
            {
                return NotFound();
            }
            int FIID = Convert.ToInt32(ht["FIID"].ToString());
            if (FIID != 0)
            {
                return await _dbContext.PMS_FixedIncomeMaster.Where(i => i.FIID == FIID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_FixedIncomeMaster.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserDtls>>> SearchByID(Hashtable ht)
        {
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            return await _dbContext.PMS_User.Where(i => i.Adminlndicator != true && i.LoginID == LoginID).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserDtls>> Authenticate(Hashtable ht)
        {
            try
            {
                UserDtls userDtls = new UserDtls();
                string Email = ht["Email"].ToString();
                string password = ht["Password"].ToString();
                if (_dbContext.PMS_User == null)
                {
                    return NotFound();
                }
                var objuserdtls = _dbContext.PMS_User.Where(i => i.EmailID == Email && i.Password == password).FirstOrDefault<UserDtls>();

                //var objuserdtls = _dbContext.PMS_User.SingleOrDefault(i => i.EmailID == Email && i.Password == password);
                if (objuserdtls == null)
                {
                    userDtls.LoginID = 0;
                    return userDtls;
                }
                else
                {
                    return objuserdtls;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        public async Task<ActionResult<UserDtls>> GetUserDtlsByID(Hashtable ht)
        {
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            var objuserdtls = _dbContext.PMS_User.SingleOrDefault(i => i.LoginID == LoginID);
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                return objuserdtls;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteUserDtlsByID(Hashtable ht)
        {
            string result = string.Empty;
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            UserDtls userdtls = new UserDtls() { LoginID = LoginID };
            _dbContext.PMS_User.Attach(userdtls);
            _dbContext.PMS_User.Remove(userdtls);
            var objuserdtls = _dbContext.SaveChanges();
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objuserdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> InsertUserDtls(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                UserDtls objuserdtls = new UserDtls();
                objuserdtls.LoginID = Convert.ToInt32(ht["UserID"].ToString());
                objuserdtls.FirstName = ht["FirstName"].ToString();
                objuserdtls.Lastname = ht["LastName"].ToString();
                objuserdtls.Gender = ht["Gender"].ToString();
                objuserdtls.Phone = ht["Phone"].ToString();
                objuserdtls.EmailID = ht["Email"].ToString();
                objuserdtls.DOB = Convert.ToDateTime(ht["DOB"].ToString());
                objuserdtls.State = ht["State"].ToString();
                objuserdtls.City = ht["City"].ToString();
                objuserdtls.Country = ht["Country"].ToString();
                objuserdtls.Pincode = Convert.ToInt64(ht["Pincode"].ToString());
                objuserdtls.Address = ht["Address"].ToString();
                objuserdtls.Password = ht["Password"].ToString();
                objuserdtls.ValidUserlndicator = Convert.ToBoolean(ht["ValidUser"].ToString());
                objuserdtls.Adminlndicator = Convert.ToBoolean(ht["AdminFlag"].ToString());

                _dbContext.PMS_User.Add(objuserdtls);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateUserDtls(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                UserDtls objuserdtls = new UserDtls();
                objuserdtls.LoginID = Convert.ToInt32(ht["UserID"].ToString());
                objuserdtls.FirstName = ht["FirstName"].ToString();
                objuserdtls.Lastname = ht["LastName"].ToString();
                objuserdtls.Gender = ht["Gender"].ToString();
                objuserdtls.Phone = ht["Phone"].ToString();
                objuserdtls.EmailID = ht["Email"].ToString();
                objuserdtls.DOB = Convert.ToDateTime(ht["DOB"].ToString());
                objuserdtls.State = ht["State"].ToString();
                objuserdtls.City = ht["City"].ToString();
                objuserdtls.Country = ht["Country"].ToString();
                objuserdtls.Pincode = Convert.ToInt64(ht["Pincode"].ToString());
                objuserdtls.Address = ht["Address"].ToString();


                var ValidUser = _dbContext.PMS_User.Where(i => i.LoginID == objuserdtls.LoginID).ToList();
                ValidUser.ForEach(i => i.FirstName = objuserdtls.FirstName);
                ValidUser.ForEach(i => i.Lastname = objuserdtls.Lastname);
                ValidUser.ForEach(i => i.Gender = objuserdtls.Gender);
                ValidUser.ForEach(i => i.Phone = objuserdtls.Phone);
                ValidUser.ForEach(i => i.EmailID = objuserdtls.EmailID);
                ValidUser.ForEach(i => i.DOB = objuserdtls.DOB);
                ValidUser.ForEach(i => i.State = objuserdtls.State);
                ValidUser.ForEach(i => i.City = objuserdtls.City);
                ValidUser.ForEach(i => i.Country = objuserdtls.Country);
                ValidUser.ForEach(i => i.Pincode = objuserdtls.Pincode);
                ValidUser.ForEach(i => i.Address = objuserdtls.Address);

                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;


                //_dbContext.PMS_User.Add(objuserdtls);
                //var result = await _dbContext.SaveChangesAsync();

                //if (result == 1)
                //{
                //    Response = "Success";
                //}
                //return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> getlistcount()
        {
            string Response = string.Empty;
            try
            {
                var User = _dbContext.PMS_User.ToList();
                var Stock = _dbContext.PMS_StockMaster.ToList();
                var MF = _dbContext.PMS_MutualFundMaster.ToList();
                var FI = _dbContext.PMS_FixedIncomeMaster.ToList();
                
                Response = User.Count + "~" + Stock.Count + "~" + MF.Count + "~" + FI.Count;
                return Response;
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<ActionResult<string>> UpdatePassword(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {

                int LoginID = Convert.ToInt32(ht["UserID"].ToString());
                string newpass = ht["NewPass"].ToString();
                string oldpass = ht["OldPass"].ToString();


                var ValidUser = _dbContext.PMS_User.Where(i => i.LoginID == LoginID && i.Password == oldpass).ToList();
                if (ValidUser.Count != 0)
                {
                    ValidUser.ForEach(i => i.Password = newpass);
                    var result = await _dbContext.SaveChangesAsync();
                    if (result == 1)
                    {
                        Response = "Success";
                    }
                }
                else
                {
                    Response = "Incorrect Old Password, Try Again";
                }


                return Response;


                //_dbContext.PMS_User.Add(objuserdtls);
                //var result = await _dbContext.SaveChangesAsync();

                //if (result == 1)
                //{
                //    Response = "Success";
                //}
                //return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveStockDtls(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_StockMaster objstockmaster = new PMS_StockMaster();
                objstockmaster.StockID = Convert.ToInt32(ht["stockid"].ToString());
                objstockmaster.StockName = ht["stockname"].ToString();
                objstockmaster.FaceValue = Convert.ToDecimal(ht["facevalue"].ToString());


                _dbContext.PMS_StockMaster.Add(objstockmaster);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveMFDtls(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_MutualFundMaster objFMmaster = new PMS_MutualFundMaster();
                objFMmaster.MFID = Convert.ToInt32(ht["MFID"].ToString());
                objFMmaster.MFName = ht["MFName"].ToString();
                objFMmaster.FundHouse = ht["MFHouse"].ToString();
                objFMmaster.FundType = ht["MFType"].ToString();
                objFMmaster.FaceValue = Convert.ToDecimal(ht["FaceValue"].ToString());


                _dbContext.PMS_MutualFundMaster.Add(objFMmaster);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveFIDtls(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_FixedIncomeMaster objFImaster = new PMS_FixedIncomeMaster();
                objFImaster.FIID = Convert.ToInt32(ht["FIID"].ToString());
                objFImaster.FIName = ht["FIName"].ToString();
                objFImaster.FIDescription = ht["FIDesc"].ToString();
                objFImaster.RateOfInterest = Convert.ToDecimal(ht["FIROI"].ToString());
                objFImaster.Tenure = Convert.ToDecimal(ht["FITenure"].ToString());
                objFImaster.PurchaseUnitValue = Convert.ToDecimal(ht["FIPUV"].ToString());


                _dbContext.PMS_FixedIncomeMaster.Add(objFImaster);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PMS_MutualFundMaster>> GetMFByID(Hashtable ht)
        {
            int MFID = Convert.ToInt32(ht["MFID"].ToString());
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            var objuserdtls = _dbContext.PMS_MutualFundMaster.SingleOrDefault(i => i.MFID == MFID);
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                return objuserdtls;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PMS_FixedIncomeMaster>> GetFIByID(Hashtable ht)
        {
            int FIID = Convert.ToInt32(ht["FIID"].ToString());
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            var objuserdtls = _dbContext.PMS_FixedIncomeMaster.SingleOrDefault(i => i.FIID == FIID);
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                return objuserdtls;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteMFByID(Hashtable ht)
        {
            string result = string.Empty;
            int MFID = Convert.ToInt32(ht["MFID"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            PMS_MutualFundMaster MFdtls = new PMS_MutualFundMaster() { MFID = MFID };
            _dbContext.PMS_MutualFundMaster.Attach(MFdtls);
            _dbContext.PMS_MutualFundMaster.Remove(MFdtls);
            var objMFdtls = _dbContext.SaveChanges();
            if (objMFdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objMFdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteFIByID(Hashtable ht)
        {
            string result = string.Empty;
            int FIID = Convert.ToInt32(ht["FIID"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            PMS_FixedIncomeMaster FIdtls = new PMS_FixedIncomeMaster() { FIID = FIID };
            _dbContext.PMS_FixedIncomeMaster.Attach(FIdtls);
            _dbContext.PMS_FixedIncomeMaster.Remove(FIdtls);
            var objMFdtls = _dbContext.SaveChanges();
            if (objMFdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objMFdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_UserStockMap>>> GetUserStockDtls(Hashtable ht)
        {
            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (LoginID != 0)
            {
                return await _dbContext.PMS_UserStockMap.Where(i => i.LoginID == LoginID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_UserStockMap.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_UserMFMap>>> GetUserMF(Hashtable ht)
        {
            if (_dbContext.PMS_UserMFMap == null)
            {
                return await _dbContext.PMS_UserMFMap.ToListAsync();
            }
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (LoginID != 0)
            {
                return await _dbContext.PMS_UserMFMap.Where(i => i.LoginId == LoginID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_UserMFMap.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PMS_UserFixedMap>>> GetUserFI(Hashtable ht)
        {
            if (_dbContext.PMS_UserFixedMap == null)
            {
                return await _dbContext.PMS_UserFixedMap.ToListAsync();
            }
            int LoginID = Convert.ToInt32(ht["LoginID"].ToString());
            if (LoginID != 0)
            {
                return await _dbContext.PMS_UserFixedMap.Where(i => i.LoginId == LoginID).ToListAsync();
            }
            else
            {
                return await _dbContext.PMS_UserFixedMap.ToListAsync();
            }
        }


        [HttpPost]
        public async Task<ActionResult<string>> AddStockuser(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserStockMap objuserstockmap = new PMS_UserStockMap();
                objuserstockmap.StockID = Convert.ToInt32(ht["StockID"].ToString());
                objuserstockmap.LoginID = Convert.ToInt32(ht["LoginID"].ToString());
                objuserstockmap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objuserstockmap.PurchasePrice = Convert.ToDecimal(ht["Price"].ToString());
                objuserstockmap.Quantity = Convert.ToDecimal(ht["Qty"].ToString());
                objuserstockmap.TransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objuserstockmap.StockName = ht["stockName"].ToString();


                _dbContext.PMS_UserStockMap.Add(objuserstockmap);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddFIuser(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserFixedMap objFIMap = new PMS_UserFixedMap();
                objFIMap.FITransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objFIMap.FIID = Convert.ToInt32(ht["FIID"].ToString());
                objFIMap.LoginId = Convert.ToInt32(ht["LoginID"].ToString());
                objFIMap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objFIMap.PurchaseQty = Convert.ToDecimal(ht["Qty"].ToString());
                objFIMap.FIName = ht["FIName"].ToString();


                _dbContext.PMS_UserFixedMap.Add(objFIMap);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<ActionResult<string>> AddMFuser(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserMFMap objuserMFMap = new PMS_UserMFMap();
                objuserMFMap.MFTransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objuserMFMap.MFID = Convert.ToInt32(ht["MFID"].ToString());
                objuserMFMap.LoginId = Convert.ToInt32(ht["LoginID"].ToString());
                objuserMFMap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objuserMFMap.Quantity = Convert.ToDecimal(ht["Qty"].ToString());
                objuserMFMap.PurchaseQty = Convert.ToDecimal(ht["PurQty"].ToString());
                objuserMFMap.MFName = ht["MFName"].ToString();
                objuserMFMap.FolioNo = ht["Folio"].ToString();


                _dbContext.PMS_UserMFMap.Add(objuserMFMap);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateUserStock(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserStockMap objuserstockmap = new PMS_UserStockMap();
                objuserstockmap.StockID = Convert.ToInt32(ht["StockID"].ToString());
                objuserstockmap.LoginID = Convert.ToInt32(ht["LoginID"].ToString());
                objuserstockmap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objuserstockmap.PurchasePrice = Convert.ToDecimal(ht["Price"].ToString());
                objuserstockmap.Quantity = Convert.ToDecimal(ht["Qty"].ToString());
                objuserstockmap.TransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objuserstockmap.StockName = ht["stockName"].ToString();

                var ValidCustomers = _dbContext.PMS_UserStockMap.Where(i => i.StockID == objuserstockmap.StockID && i.LoginID == objuserstockmap.LoginID && i.TransactionNo == objuserstockmap.TransactionNo).ToList();
                ValidCustomers.ForEach(i => i.Quantity = objuserstockmap.Quantity);

                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateUserMF(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserMFMap objMFUserMap = new PMS_UserMFMap();
                objMFUserMap.MFTransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objMFUserMap.LoginId = Convert.ToInt32(ht["LoginID"].ToString());
                objMFUserMap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objMFUserMap.MFID = Convert.ToInt32(ht["MFID"].ToString());
                objMFUserMap.Quantity = Convert.ToDecimal(ht["Qty"].ToString());
                objMFUserMap.PurchaseQty = Convert.ToDecimal(ht["PurQty"].ToString());

                var ValidCustomers = _dbContext.PMS_UserMFMap.Where(i => i.MFID == objMFUserMap.MFID && i.LoginId == objMFUserMap.LoginId && i.MFTransactionNo == objMFUserMap.MFTransactionNo).ToList();
                ValidCustomers.ForEach(i => i.Quantity = objMFUserMap.Quantity);
                ValidCustomers.ForEach(i => i.PurchaseQty = objMFUserMap.PurchaseQty);
                ValidCustomers.ForEach(i => i.PurchaseDate = objMFUserMap.PurchaseDate);

                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateUserFI(Hashtable ht)
        {
            string Response = string.Empty;
            try
            {
                PMS_UserFixedMap objFIUserMap = new PMS_UserFixedMap();
                objFIUserMap.FITransactionNo = Convert.ToInt32(ht["TransactionNo"].ToString());
                objFIUserMap.FIID = Convert.ToInt32(ht["FIID"].ToString());
                objFIUserMap.LoginId = Convert.ToInt32(ht["LoginID"].ToString());
                objFIUserMap.PurchaseDate = Convert.ToDateTime(ht["Date"].ToString());
                objFIUserMap.PurchaseQty = Convert.ToDecimal(ht["Qty"].ToString());

                var ValidCustomers = _dbContext.PMS_UserFixedMap.Where(i => i.FIID == objFIUserMap.FIID && i.LoginId == objFIUserMap.LoginId && i.FITransactionNo == objFIUserMap.FITransactionNo).ToList();
                ValidCustomers.ForEach(i => i.PurchaseQty = objFIUserMap.PurchaseQty);
                ValidCustomers.ForEach(i => i.PurchaseDate = objFIUserMap.PurchaseDate);

                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    Response = "Success";
                }
                return Response;
                //return CreatedAtAction(nameof(InsertUserDtls), objuserdtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteUserMF(Hashtable ht)
        {
            string result = string.Empty;
            int LoginID = Convert.ToInt32(ht["userid"].ToString());
            int transid = Convert.ToInt32(ht["transid"].ToString());
            int MFID = Convert.ToInt32(ht["MFID"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            PMS_UserMFMap MFdtls = new PMS_UserMFMap() { LoginId = LoginID, MFID = MFID, MFTransactionNo = transid };
            _dbContext.PMS_UserMFMap.Attach(MFdtls);
            _dbContext.PMS_UserMFMap.Remove(MFdtls);
            var objuserdtls = _dbContext.SaveChanges();
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objuserdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteUserFI(Hashtable ht)
        {
            string result = string.Empty;
            int LoginID = Convert.ToInt32(ht["userid"].ToString());
            int transid = Convert.ToInt32(ht["transid"].ToString());
            int FIID = Convert.ToInt32(ht["FIID"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            PMS_UserFixedMap MFdtls = new PMS_UserFixedMap() { LoginId = LoginID, FIID = FIID, FITransactionNo = transid };
            _dbContext.PMS_UserFixedMap.Attach(MFdtls);
            _dbContext.PMS_UserFixedMap.Remove(MFdtls);
            var objuserdtls = _dbContext.SaveChanges();
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objuserdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteUserStock(Hashtable ht)
        {
            string result = string.Empty;
            int LoginID = Convert.ToInt32(ht["userid"].ToString());
            int transid = Convert.ToInt32(ht["transid"].ToString());
            int stockid = Convert.ToInt32(ht["stockid"].ToString());

            if (_dbContext.PMS_User == null)
            {
                return NotFound();
            }

            PMS_UserStockMap stockdtls = new PMS_UserStockMap() { LoginID = LoginID, StockID = stockid, TransactionNo = transid };
            _dbContext.PMS_UserStockMap.Attach(stockdtls);
            _dbContext.PMS_UserStockMap.Remove(stockdtls);
            var objuserdtls = _dbContext.SaveChanges();
            if (objuserdtls == null)
            {
                return NotFound();
            }
            else
            {
                if (objuserdtls == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }

            }
        }



    }
}
