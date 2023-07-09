using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Home
    {
    }

    public class UserDtls
    {
        public int LoginID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Adminlndicator { get; set; }
        public string ValidUserlndicator { get; set; }
        public string Result { get; set; }
    }
    public class lstUserDtls
    {
        public List<UserDtls> listUserDtls { get; set; }
    }

    public class PMS_StockMaster
    {
        public int StockID { get; set; }
        public string StockName { get; set; }
        public decimal FaceValue { get; set; }
    }

    public class PMS_UserStockMap
    {
        public int TransactionNo { get; set; }
        public int StockID { get; set; }
        public string StockName { get; set; }
        public int LoginID { get; set; }
        public decimal Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }

    public class PMS_MutualFundMaster
    {
        public int MFID { get; set; }
        public string MFName { get; set; }
        public string FundHouse { get; set; }
        public string FundType { get; set; }
        public decimal FaceValue { get; set; }
    }

    public class PMS_UserMFMap
    {
        public int MFTransactionNo { get; set; }
        public int MFID { get; set; }
        public string MFName { get; set; }
        public int LoginId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseQty { get; set; }
        public string FolioNo { get; set; }
    }

    public class PMS_FixedIncomeMaster
    {
        public int FIID { get; set; }
        public string FIName { get; set; }
        public string FIDescription { get; set; }
        public decimal RateOfInterest { get; set; }
        public decimal Tenure { get; set; }
        public decimal PurchaseUnitValue { get; set; }
    }

    public class PMS_UserFixedMap
    {
        public int FITransactionNo { get; set; }
        public int FIID { get; set; }
        public string FIName { get; set; }
        public int LoginId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseQty { get; set; }
    }

    public class lstStockDtls
    {
        public List<PMS_StockMaster> listStockDtls { get; set; }
    }

    public class lstUserStockDtls
    {
        public List<PMS_UserStockMap> listUserStockDtls { get; set; }
    }

    public class lstMFDtls
    {
        public List<PMS_MutualFundMaster> listMFDtls { get; set; }
    }

    public class lstUserMFDtls
    {
        public List<PMS_UserMFMap> listUserMFDtls { get; set; }
    }

    public class lstFIDtls
    {
        public List<PMS_FixedIncomeMaster> listFIDtls { get; set; }
    }

    public class lstUserFIDtls
    {
        public List<PMS_UserFixedMap> listUserFIDtls { get; set; }
    }

    public class viewmodel
    {
        public UserDtls userDtls { get; set; }
        public lstStockDtls lststockDtls { get; set; }
        public lstUserStockDtls lstuserstockDtls { get; set; }
        public lstMFDtls lstMFDtls { get; set; }
        public lstUserMFDtls lstUserMFDtls { get; set; }
        public lstFIDtls lstFIDtls { get; set; }
        public lstUserFIDtls lstUserFIDtls { get; set; }
    }
}
