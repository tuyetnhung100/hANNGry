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

        //    SqlCommand command = new SqlCommand("Select Name, Email from Accounts", connect);

        //    //SqlDataReader reader = command.ExecuteReader();
        //    //Subscriber mySubscriber;

        //    //while (reader.Read())
        //    //{
        //    //    mySubscriber = new Subscriber();
        //    //    mySubscriber.name = reader.GetString(0);
        //    //    mySubscriber.email = reader.GetString(1);
        //    //    subcribers.Add(mySubscriber);
        //    //}

        //    //reader.Close();

        //    connect.Close();
        //    return true;
        ////}
    }
}
