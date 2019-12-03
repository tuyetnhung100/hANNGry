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
        public const int SALT_SIZE = 24; // size in bytes
        public const int HASH_SIZE = 24; // size in bytes
        public const int ITERATIONS = 100000; // number of pbkdf2 iterations

        /// <summary>
        /// Create Salt
        /// </summary>
        /// <returns>salt</returns>
        public static string CreateSalt()
        {
            // Generate a salt.
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] saltBuffer = new byte[SALT_SIZE];
            provider.GetBytes(saltBuffer);
            string salt = Convert.ToBase64String(saltBuffer);
            return salt;
        }

        /// <summary>
        /// Create Hash
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="salt">The salt</param>
        /// <returns>hash</returns>
        public static string CreateHash(string password, string salt)
        {
            // Generate hash.
            byte[] saltBuffer = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBuffer, ITERATIONS);
            byte[] hashBuffer = pbkdf2.GetBytes(HASH_SIZE);
            string hash = Convert.ToBase64String(hashBuffer);
            return hash;
        }

        /// <summary>
        /// Add new account to DB.
        /// </summary>
        /// <param name="newAccount">The newAccount</param>
        /// <returns>Whether insert successfully</returns>
        public static bool CreateAccount(Account newAccount)
        {
            string salt = CreateSalt();
            string hash = CreateHash(newAccount.PasswordHash, salt);
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
 PhoneNumber,
 CreatedDate,
 Location,
 NotificationType,
 Code,
 Activated)
  VALUES (@Username,
          @Name,
          @Email,
          @Role,
          @PasswordHash,
          @PasswordSalt,
          @PhoneNumber,
          @Date,
          @Location,
          @NotificationType, 
          @Code,
          0);", connect);

            command.Parameters.AddWithValue("@Username", newAccount.Username);
            command.Parameters.AddWithValue("@Name", newAccount.Name);
            command.Parameters.AddWithValue("@Email", newAccount.Email);
            command.Parameters.AddWithValue("@Role", Role.Subscriber);
            command.Parameters.AddWithValue("@PasswordHash", hash);
            command.Parameters.AddWithValue("@PasswordSalt", salt);
            command.Parameters.AddWithValue("@PhoneNumber", newAccount.PhoneNumber);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@Location", newAccount.Location);
            command.Parameters.AddWithValue("@NotificationType", newAccount.NotificationType);
            command.Parameters.AddWithValue("@Code", newAccount.Code);

            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        /// <summary>
        /// Look for a user's account info in the DB using username and email (for Register).
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="email">The email</param>
        /// <returns>The Account object</returns>
        public static Account CheckAccountAvailability(string username, string email)
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
            Account account = null;

            int accountIdIndex = reader.GetOrdinal("AccountId");
            int usernameIndex = reader.GetOrdinal("Username");
            int roleIndex = reader.GetOrdinal("Role");
            int emailIndex = reader.GetOrdinal("Email");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                account = new Account();
                account.AccountId = reader.GetInt32(accountIdIndex);
                account.Username = reader.GetString(usernameIndex);
                account.Role = (Role)reader.GetInt32(roleIndex);
                account.Email = reader.GetString(emailIndex);
                account.PasswordHash = reader.GetString(passwordHashIndex);
                account.PasswordSalt = reader.GetString(passwordSaltIndex);
                account.Name = reader.GetString(nameIndex);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return account;
        }

        /// <summary>
        /// Activate accounts using Code
        /// </summary>
        /// <param name="code">The code for activation</param>
        /// <returns>Whether update successfully</returns>
        public static bool ActivateAccount(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET Activated = 1,
    Code = NULL
WHERE Code = @Code;", connect);

            command.Parameters.AddWithValue("@Code", code);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }

        /// <summary>
        /// Look for a user's account info in the DB using username (for Login).
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns>The Account object</returns>
        public static Account FindActivatedAccount(string username)
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
  Location,
  PhoneNumber
FROM Accounts
WHERE Activated = 1
AND Username = @Username;", connect);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();
            Account account = null;

            int accountIdIndex = reader.GetOrdinal("AccountId");
            int usernameIndex = reader.GetOrdinal("Username");
            int roleIndex = reader.GetOrdinal("Role");
            int emailIndex = reader.GetOrdinal("Email");
            int passwordHashIndex = reader.GetOrdinal("PasswordHash");
            int passwordSaltIndex = reader.GetOrdinal("PasswordSalt");
            int nameIndex = reader.GetOrdinal("Name");
            int notificationTypeIndex = reader.GetOrdinal("NotificationType");
            int locationIndex = reader.GetOrdinal("Location");
            int PhoneNumberIndex = reader.GetOrdinal("PhoneNumber");

            while (reader.Read())
            {
                account = new Account();
                account.AccountId = reader.GetInt32(accountIdIndex);
                account.Username = reader.GetString(usernameIndex);
                account.Role = (Role)reader.GetInt32(roleIndex);
                account.Email = reader.GetString(emailIndex);
                account.PasswordHash = reader.GetString(passwordHashIndex);
                account.PasswordSalt = reader.GetString(passwordSaltIndex);
                account.Name = reader.GetString(nameIndex);
                account.NotificationType = (NotificationType)reader.GetInt32(notificationTypeIndex);
                account.Location = (Location)reader.GetInt32(locationIndex);
                account.PhoneNumber = reader.GetString(PhoneNumberIndex);
                account.Activated = true;
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return account;
        }

        /// <summary>
        /// Look for a user's account info in the DB using email or phoneNumber (for UserAccount).
        /// </summary>
        /// <param name="email">The email</param>
        /// <param name="phoneNumber">The phone number</param>
        /// <returns>The CheckValidInfoResult object</returns>
        public static CheckValidInfoResult CheckValidInfo(string email, string phoneNumber)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
SELECT (SELECT
         COUNT(*)
       FROM Accounts
       WHERE Email = @Email)
       AS EmailCount,
       (SELECT
         COUNT(*)
       FROM Accounts
       WHERE PhoneNumber = @PhoneNumber)
       AS PhoneNumberCount;", connect);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            SqlDataReader reader = command.ExecuteReader();
            CheckValidInfoResult result = new CheckValidInfoResult();

            int emailCountIndex = reader.GetOrdinal("EmailCount");
            int phoneNumberCountIndex = reader.GetOrdinal("PhoneNumberCount");

            while (reader.Read())
            {
                result = new CheckValidInfoResult
                {
                    EmailCount = reader.GetInt32(emailCountIndex),
                    PhoneNumberCount = reader.GetInt32(phoneNumberCountIndex)
                };
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return result;
        }

        /// <summary>
        /// Get all combinations from location
        /// </summary>
        /// <param name="location">The location</param>
        /// <returns>The locationList</returns>
        private static List<int> GetLocationList(Location location)
        {
            bool hasSylvania = location.HasFlag(Location.Sylvania);
            bool hasRockCreek = location.HasFlag(Location.RockCreek);
            bool hasCascade = location.HasFlag(Location.Cascade);
            bool hasSoutheast = location.HasFlag(Location.Southeast);
            List<int> locationList = new List<int>();

            // there are 16 combinations
            for (int i = 0; i < 16; i++)
            {
                Location current = (Location)i;
                if (hasSylvania && current.HasFlag(Location.Sylvania))
                {
                    locationList.Add(i);
                }
                if (hasRockCreek && current.HasFlag(Location.RockCreek))
                {
                    locationList.Add(i);
                }
                if (hasCascade && current.HasFlag(Location.Cascade))
                {
                    locationList.Add(i);
                }
                if (hasSoutheast && current.HasFlag(Location.Southeast))
                {
                    locationList.Add(i);
                }
            }
            return locationList;
        }

        /// <summary>
        /// Get all Subscribers.
        /// </summary>
        /// <param name="command">The SqlCommand</param>
        /// <param name="location">The location</param>
        /// <param name="subscribers">The all subscribers</param>
        public static void GetSubscribers(SqlCommand command, Location location, ref List<Account> subscribers)
        {
            subscribers.Clear();

            List<int> locationList = GetLocationList(location);
            bool isAllLocations = (int)location == 15;
            string locationCondition = isAllLocations
                ? string.Empty
                : "AND Location IN (" + string.Join(",", locationList) + ")";
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
AND Role = @Role
AND NotificationType <> 0
" + locationCondition + @"
;";
            command.CommandText = sql;

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Role", Role.Subscriber);

            SqlDataReader reader = command.ExecuteReader();
            int accountIdIndex = reader.GetOrdinal("AccountId");
            int nameIndex = reader.GetOrdinal("Name");
            int emailIndex = reader.GetOrdinal("Email");
            int phoneNumberIndex = reader.GetOrdinal("PhoneNumber");
            int notificationTypeIndex = reader.GetOrdinal("NotificationType");
            int locationIndex = reader.GetOrdinal("Location");

            while (reader.Read())
            {
                Account account = new Account
                {
                    AccountId = reader.GetInt32(accountIdIndex),
                    Name = reader.GetString(nameIndex),
                    Email = reader.GetString(emailIndex),
                    PhoneNumber = reader.GetString(phoneNumberIndex),
                    NotificationType = (NotificationType)reader.GetInt32(notificationTypeIndex),
                    Location = (Location)reader.GetInt32(locationIndex)
                };
                subscribers.Add(account);
            }

            reader.Close();
        }

        /// <summary>
        /// Update account's name, username, email, notification type, location in DB.
        /// </summary>
        /// <param name="updatedAccount">The updated account</param>
        /// <returns>Whether update successfully</returns>
        public static bool UpdateAccount(Account updatedAccount)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET 
 Name = @Name,
 Email = @Email,
 NotificationType = @NotificationType,
 Location = @Location,
 PhoneNumber = @PhoneNumber
WHERE Activated = 1 AND AccountId = @AccountId;", connect);

            command.Parameters.AddWithValue("@Name", updatedAccount.Name);
            command.Parameters.AddWithValue("@Email", updatedAccount.Email);
            command.Parameters.AddWithValue("@NotificationType", updatedAccount.NotificationType);
            command.Parameters.AddWithValue("@Location", updatedAccount.Location);
            command.Parameters.AddWithValue("@PhoneNumber", updatedAccount.PhoneNumber);
            command.Parameters.AddWithValue("@AccountId", updatedAccount.AccountId);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }

        /// <summary>
        /// Update account's code in DB (for ChangePasswordByEmail link)
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="code">The code</param>
        /// <returns>Whether update successfully</returns>
        public static bool UpdateCode(string username, string code)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET Code = @Code
WHERE Activated = 1
AND Username = @Username;", connect);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Code", code);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }

        /// <summary>
        /// Reset Password in DB.
        /// </summary>
        /// <param name="code">The code</param>
        /// <param name="newPassword">The new password</param>
        /// <returns></returns>
        public static bool ResetPassword(string code, string newPassword)
        {
            string salt = CreateSalt();
            string hash = CreateHash(newPassword, salt);
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET 
 PasswordHash = @PasswordHash,
 PasswordSalt = @PasswordSalt,
 Code = NULL
WHERE Code = @Code;", connect);

            command.Parameters.AddWithValue("@PasswordHash", hash);
            command.Parameters.AddWithValue("@PasswordSalt", salt);
            command.Parameters.AddWithValue("@Code", code);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }

        /// <summary>
        /// Reset Password in DB.
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns></returns>
        public static bool UpdatePassword(Account account)
        {
            string salt = CreateSalt();
            string hash = CreateHash(account.PasswordHash, salt);
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Accounts
SET 
 PasswordHash = @PasswordHash,
 PasswordSalt = @PasswordSalt
WHERE AccountId = @AccountId;", connect);

            command.Parameters.AddWithValue("@PasswordHash", hash);
            command.Parameters.AddWithValue("@PasswordSalt", salt);
            command.Parameters.AddWithValue("@AccountId", account.AccountId);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }

        /// <summary>
        /// Check account by code
        /// </summary>
        /// <param name="code">The code</param>
        /// <returns>The Account object</returns>
        public static Account FindAccountByCode(string code)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
SELECT Name
FROM Accounts
WHERE Activated = 1
AND Code = @Code;", connect);
            command.Parameters.AddWithValue("@Code", ParameterHelper.GetNullableValue(code));

            SqlDataReader reader = command.ExecuteReader();
            Account account = null;

            int nameIndex = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                account = new Account();
                account.Name = reader.GetString(nameIndex);
            }

            reader.Close();
            command.ExecuteNonQuery();
            return account;
        }

        /// <summary>
        /// Delete an account in DB.
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>Whether delete successfully</returns>
        public static bool Delete(Account account)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
DELETE FROM Accounts
WHERE Activated = 1
  AND AccountId = @AccountId;", connect);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@AccountId", account.AccountId);

            int affectedRows = command.ExecuteNonQuery();
            connect.Close();
            return affectedRows == 1;
        }
    }
}
