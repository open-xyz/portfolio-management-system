using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS_Api.Models
{
    public class PMS
    {
    }

    public class UserDtls
    {
        [Key]
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
        public Int64  Pincode { get; set; }
        public string Phone { get; set; }
        public bool Adminlndicator { get; set; }
        public bool ValidUserlndicator { get; set; }
    }

    public class PMS_StockMaster
    {
        [Key]
        public int StockID { get; set; }
        public string StockName { get; set; }
        public decimal FaceValue { get; set; }
    }

    public class PMS_MutualFundMaster
    {
        [Key]
        public int MFID { get; set; }
        public string MFName { get; set; }
        public string FundHouse { get; set; }
        public string FundType { get; set; }
        public decimal FaceValue { get; set; }
    }

    public class PMS_UserMFMap
    {
        [Key]
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
        [Key]
        public int FIID { get; set; }
        public string FIName { get; set; }
        public string FIDescription { get; set; }
        public decimal RateOfInterest { get; set; }
        public decimal Tenure { get; set; }
        public decimal PurchaseUnitValue { get; set; }
    }

    public class PMS_UserStockMap
    {
        [Key]
        public int TransactionNo { get; set; }
        public int StockID { get; set; }
        public string StockName { get; set; }
        public int LoginID { get; set; }
        public decimal Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }

    public class PMS_UserFixedMap
    {
        [Key]
        public int FITransactionNo { get; set; }
        public int FIID { get; set; }
        public string FIName { get; set; }
        public int LoginId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseQty { get; set; }
    }
}
