using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace PMS_Api.Models
{
    public class PMSContext:DbContext
    {
        public PMSContext(DbContextOptions<PMSContext> options):base(options) 
        {
            
        }

        public DbSet<UserDtls> PMS_User { get; set; }
        public DbSet<PMS_StockMaster> PMS_StockMaster { get; set; }
        public DbSet<PMS_UserStockMap> PMS_UserStockMap { get; set; }
        public DbSet<PMS_MutualFundMaster> PMS_MutualFundMaster { get; set; }
        public DbSet<PMS_UserMFMap> PMS_UserMFMap { get; set; }
        public DbSet<PMS_FixedIncomeMaster> PMS_FixedIncomeMaster { get; set; }
        public DbSet<PMS_UserFixedMap> PMS_UserFixedMap { get; set; }
    }
}
