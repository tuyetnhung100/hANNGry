using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AccountLibrary
{
    public class AccountDB
    {
        // It will be removed when DBConnect project is merged.
        private static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
        }

        public static bool Load(ref List<Account> accounts)
        {
            return true;
        }

        // Return first data just for test right now
        // todo: load real logined staff
        public static bool GetLoginedStaff(ref Account account)
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Open();

                string sql = @"
SELECT TOP 1
  AccountId,
  Name,
  Email
FROM Accounts
WHERE Role = @Role;";
                SqlCommand command = new SqlCommand(sql, connection);

                // set Parameters
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Role", Role.Subscriber);

                SqlDataReader reader = command.ExecuteReader();

                int accountId = reader.GetOrdinal("AccountId");
                int name = reader.GetOrdinal("Name");
                int email = reader.GetOrdinal("Email");

                while (reader.Read())
                {
                    account.AccountId = reader.GetInt32(accountId);
                    account.Name = reader.GetString(name);
                    account.Email = reader.GetString(email);
                }

                reader.Close();

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
