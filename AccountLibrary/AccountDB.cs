/*
 * Programmer(s):      Gong-Hao
 * Date:               10/23/2019
 * What the code does: Data access layer of Account.
 */

using System;
using System.Collections.Generic;
using ConnectDB;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AccountLibrary
{
    public class AccountDB
    {

        public static Boolean Add(Account myAccount)
        {
            string salt = CreateSalt();
            string hash = CreateHash(myAccount.PasswordHash, salt);
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Insert into Accounts(Username, Name, Email, Role, PasswordHash, PasswordSalt, CreatedDate)" +
                " Values(@Username, @Name, @Email, 0, @PasswordHash, @PasswordSalt, @Date)", connect);

            command.Parameters.AddWithValue("@Username", myAccount.Username);
            command.Parameters.AddWithValue("@Name", myAccount.Name);
            command.Parameters.AddWithValue("@Email", myAccount.Email);
            command.Parameters.AddWithValue("@PasswordHash", hash);
            command.Parameters.AddWithValue("@PasswordSalt", salt);
            command.Parameters.AddWithValue("@Date", DateTime.Now);

            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        // Look for a user's account info in the DB using username and email (for Register)
        public static Account FindAccount(string username, string email)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select Username, Email, PasswordHash, PasswordSalt, Name from Accounts " +
                "where Username = @username OR Email = @email", connect);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.Username = reader.GetString(0);
                myAccount.Email = reader.GetString(1);
                myAccount.PasswordHash = reader.GetString(2);
                myAccount.PasswordSalt = reader.GetString(3);
                myAccount.Name = reader.GetString(4);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return myAccount;
        }

        // Look for a user's account info in the DB using username (for Login)
        public static Account FindAccount(string username)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select Username, Role, PasswordHash, PasswordSalt, Name from Accounts " +
                "where Username = @username", connect);
            command.Parameters.AddWithValue("@username", username);          
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.Username = reader.GetString(0);
                myAccount.Role = (Role)reader.GetInt32(1);
                myAccount.PasswordHash = reader.GetString(2);
                myAccount.PasswordSalt = reader.GetString(3);
                myAccount.Name = reader.GetString(4);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return myAccount;
        }

        // Return first data just for test right now
        // todo: load real logined staff
        public static bool FakeGetLoginedStaff(ref Account account)
        {
            try
            {
                SqlConnection connection = DBConnect.GetConnection();
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
                command.Parameters.AddWithValue("@Role", Role.Employee);

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

        /// <summary>
        /// SELECT all Subscribers.
        /// </summary>
        /// <param name="command">The SqlCommand</param>
        /// <param name="subscribers">The all subscribers</param>
        public static void GetSubscribers(SqlCommand command, ref List<Account> subscribers)
        {
            subscribers.Clear();

            // set select sql
            string sql = @"
SELECT
  AccountId,
  Name,
  Email
FROM Accounts
WHERE Role = @Role;";
            command.CommandText = sql;

            // set Parameters
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Role", Role.Subscriber);

            // read data into a int list
            SqlDataReader reader = command.ExecuteReader();
            int accountId = reader.GetOrdinal("AccountId");
            int name = reader.GetOrdinal("Name");
            int email = reader.GetOrdinal("Email");

            while (reader.Read())
            {
                Account account = new Account
                {
                    AccountId = reader.GetInt32(accountId),
                    Name = reader.GetString(name),
                    Email = reader.GetString(email)
                };
                subscribers.Add(account);
            }

            reader.Close();
        }

        public const int SALT_SIZE = 24; // size in bytes
        public const int HASH_SIZE = 24; // size in bytes
        public const int ITERATIONS = 100000; // number of pbkdf2 iterations

        public static string CreateSalt()
        {
            // Generate a salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] saltBuffer = new byte[SALT_SIZE];
            provider.GetBytes(saltBuffer);
            string salt = Convert.ToBase64String(saltBuffer);
            return salt;
        }

        public static string CreateHash(string password, string salt)
        {
            // Generate hash
            byte[] saltBuffer = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBuffer, ITERATIONS);
            byte[] hashBuffer = pbkdf2.GetBytes(HASH_SIZE);
            string hash = Convert.ToBase64String(hashBuffer);
            return hash;
        }
    }
}
