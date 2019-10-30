using System.Data.SqlClient;

namespace ConnectDB
{
    public class DBConnect
    {
        /// <summary>
        /// Connects to the DB with the credentials.
        /// </summary>
        /// <returns>The SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?;");
            return connect;
        }
    }
}
