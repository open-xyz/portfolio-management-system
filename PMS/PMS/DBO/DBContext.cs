using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.DBO
{
    public class DBContext
    {
        private static string connectionstring;

        static DBContext()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

            connectionstring = config.GetSection("ConnectionString").Value;
        }

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            return con;
        }
    }
}
