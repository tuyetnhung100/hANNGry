using System;
using System.Collections.Generic;
using DBConnect;
using System.Data.SqlClient;

namespace AccountLibrary
{
    public class AccountDB
    {
        public static Boolean Add(Account myAccount)
        {
            SqlConnection connect = ConnectDB.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Insert into Accounts(Name, Email, Role, PasswordHash, PasswordSalt, CreatedDate)" +
                " Values(@Name, @Email, 0, @PasswordHash, ' ', @Date)", connect);

            command.Parameters.AddWithValue("@Name", myAccount.Name);
            command.Parameters.AddWithValue("@Email", myAccount.Email);
            command.Parameters.AddWithValue("@PasswordHash", myAccount.PasswordHash);
            command.Parameters.AddWithValue("@Date", DateTime.Now);

            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        //public static Boolean Load(ref List<Account> accounts)
        //{
        //    SqlConnection connect = ConnectDB.GetConnection();
        //    connect.Open();                                                             

        //    SqlCommand command = new SqlCommand("Select Email, PasswordHash from Accounts", connect);
        //    SqlDataReader reader = command.ExecuteReader();
        //    Account myAccount;

        //    while (reader.Read())
        //    {
        //        myAccount = new Account();
        //        myAccount.Email = reader.GetString(0);
        //        myAccount.PasswordHash = reader.GetString(1);
        //    }

        //    reader.Close();
        //    command.ExecuteNonQuery();
        //    connect.Close();
        //    return true;
        //}

        public static Account FindAccount(string email)
        {
            SqlConnection connect = ConnectDB.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select Email, PasswordHash, PasswordSalt from Accounts where Email = @email", connect);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = command.ExecuteReader();
            Account myAccount = null;

            while (reader.Read())
            {
                myAccount = new Account();
                myAccount.Email = reader.GetString(0);
                myAccount.PasswordHash = reader.GetString(1);
                myAccount.PasswordSalt = reader.GetString(2);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connect.Close();
            return myAccount;
        }
    }
}
