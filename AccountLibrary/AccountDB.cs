/*
 * Programmer(s):      Gong-Hao, Nina Hoang
 * Date:               10/23/2019
 * What the code does: Data access layer of Account.
 */

using ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace AccountLibrary
{
    public class AccountDB
    {
        public static bool Add(Account myAccount)
        {
            string salt = CreateSalt();
            string hash = CreateHash(myAccount.PasswordHash, salt);
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
INSERT INTO Accounts
(Username,
 Name,
 Email,
 Role,
 PasswordHash,
 PasswordSalt,
 CreatedDate)
  VALUES (@Username,
          @Name,
          @Email,
          @Role,
          @PasswordHash,
          @PasswordSalt,
          @Date);", connect);

            command.Parameters.AddWithValue("@Username", myAccount.Username);
            command.Parameters.AddWithValue("@Name", myAccount.Name);
            command.Parameters.AddWithValue("@Email", myAccount.Email);
            command.Parameters.AddWithValue("@Role", Role.Subscriber);
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

            SqlCommand command = new SqlCommand(@"
SELECT
  AccountId,
  Username,
  Email,
  PasswordHash,
  PasswordSalt,
  Name
FROM Accounts
WHERE Username = @Username
OR Email = @Email;", connect);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            int accountIdIndex = reader.GetOrdinal("AccountId");
            int usernameIndex = reader.GetOrdinal("Username");
            int roleIndex = reader.GetOrdinal("Role");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.AccountId = reader.GetInt32(accountIdIndex);
                myAccount.Username = reader.GetString(usernameIndex);
                myAccount.Role = (Role)reader.GetInt32(roleIndex);
                myAccount.PasswordHash = reader.GetString(passwordHashIndex);
                myAccount.PasswordSalt = reader.GetString(passwordSaltIndex);
                myAccount.Name = reader.GetString(nameIndex);
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

            SqlCommand command = new SqlCommand(@"
SELECT
  AccountId,
  Username,
  Role,
  PasswordHash,
  PasswordSalt,
  Name
FROM Accounts
WHERE Username = @Username;", connect);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            int accountIdIndex = reader.GetOrdinal("AccountId");
            int usernameIndex = reader.GetOrdinal("Username");
            int roleIndex = reader.GetOrdinal("Role");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.AccountId = reader.GetInt32(accountIdIndex);
                myAccount.Username = reader.GetString(usernameIndex);
                myAccount.Role = (Role)reader.GetInt32(roleIndex);
                myAccount.PasswordHash = reader.GetString(passwordHashIndex);
                myAccount.PasswordSalt = reader.GetString(passwordSaltIndex);
                myAccount.Name = reader.GetString(nameIndex);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return myAccount;
        }

        // Return first data just for test right now
        // todo: load real logined employee
        public static bool FakeGetLoginedEmployee(ref Account account)
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
