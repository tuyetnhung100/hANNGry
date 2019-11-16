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
  Role,
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
            int emailIndex = reader.GetOrdinal("Email");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.AccountId = reader.GetInt32(accountIdIndex);
                myAccount.Username = reader.GetString(usernameIndex);
                myAccount.Role = (Role)reader.GetInt32(roleIndex);
                myAccount.Email = reader.GetString(emailIndex);
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
  Email,
  PasswordHash,
  PasswordSalt,
  Name,
  NotificationType,
  Location
FROM Accounts
WHERE Username = @Username;", connect);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            int accountIdIndex = reader.GetOrdinal("AccountId");
            int usernameIndex = reader.GetOrdinal("Username");
            int roleIndex = reader.GetOrdinal("Role");
            int emailIndex = reader.GetOrdinal("Email");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");
            int notificationTypeIndex = reader.GetOrdinal("NotificationType");
            int locationIndex = reader.GetOrdinal("Location");

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.AccountId = reader.GetInt32(accountIdIndex);
                myAccount.Username = reader.GetString(usernameIndex);
                myAccount.Role = (Role)reader.GetInt32(roleIndex);
                myAccount.Email = reader.GetString(emailIndex);
                myAccount.PasswordHash = reader.GetString(passwordHashIndex);
                myAccount.PasswordSalt = reader.GetString(passwordSaltIndex);
                myAccount.Name = reader.GetString(nameIndex);
                myAccount.NotificationType = (NotificationType)reader.GetInt32(notificationTypeIndex);
                myAccount.Location = (Location)reader.GetInt32(locationIndex);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return myAccount;
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
  Email,
  PhoneNumber,
  NotificationType,
  Location
FROM Accounts
WHERE Activated = 1
AND Role = @Role;";
            command.CommandText = sql;

            // set Parameters
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Role", Role.Subscriber);

            // read data into a int list
            SqlDataReader reader = command.ExecuteReader();
            int accountId = reader.GetOrdinal("AccountId");
            int name = reader.GetOrdinal("Name");
            int email = reader.GetOrdinal("Email");
            int phoneNumber = reader.GetOrdinal("PhoneNumber");
            int notificationType = reader.GetOrdinal("NotificationType");
            int location = reader.GetOrdinal("Location");

            while (reader.Read())
            {
                Account account = new Account
                {
                    AccountId = reader.GetInt32(accountId),
                    Name = reader.GetString(name),
                    Email = reader.GetString(email),
                    PhoneNumber = reader.GetString(phoneNumber),
                    NotificationType = (NotificationType)reader.GetInt32(notificationType),
                    Location = (Location)reader.GetInt32(location)
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

        public static bool Update(Account updatedAccount)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET 
 Username = @Username,
 Name = @Name,
 Email = @Email,
 NotificationType = @NotificationType,
 Location = @Location
WHERE AccountId = @AccountId;", connect);

            command.Parameters.AddWithValue("@Username", updatedAccount.Username);
            command.Parameters.AddWithValue("@Name", updatedAccount.Name);
            command.Parameters.AddWithValue("@Email", updatedAccount.Email);
            command.Parameters.AddWithValue("@NotificationType", updatedAccount.NotificationType);
            command.Parameters.AddWithValue("@Location", updatedAccount.Location);
            command.Parameters.AddWithValue("@AccountId", updatedAccount.AccountId);

            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }
    }
}
