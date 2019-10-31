using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectDB
{
    // Connects to the DB with the credentials.
    public class DBConnect
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
        }
    }
}
