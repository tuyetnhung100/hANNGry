using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectDB
{
    public class DBConnect
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
        }
    }
}
