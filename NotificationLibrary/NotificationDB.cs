using System;
using System.Collections.Generic;
using DBConnect1;
using System.Data.SqlClient;

namespace NotificationLibrary
{
    public class NotificationDB
    {
        public static Boolean Load(DateTime start, DateTime end, ref List<Notification> notifications)
        {
            SqlConnection connect = ConnectDB.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select Subject, Message, SentAccountId, SentDate from Notifications where SentDate > @start and SentDate < @end ", connect);

            command.Parameters.AddWithValue("@start", start);
            command.Parameters.AddWithValue("@end", end);

            SqlDataReader reader = command.ExecuteReader();

            Notification myNotification;
            while (reader.Read())
            {
                myNotification = new Notification();
                myNotification.Subject = reader.GetString(0);
                myNotification.Message = reader.GetString(1);
                myNotification.SentAccountId = reader.GetInt32(2);
                myNotification.SentDate = reader.GetDateTime(3);
                notifications.Add(myNotification);
            }

            reader.Close();

            connect.Close();

            //LoadFakeNotifications(ref notifications);
            return true;
        }
        private static void LoadFakeNotifications(ref List<Notification> notifications)
        {
            Notification note1 = new Notification();
            note1.Subject = "Food";
            note1.Message = "BLAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" + Environment.NewLine + "BLAAAHHHHHHHHHHHHHHHHAAH";
            note1.SentDate = DateTime.Now;

            Notification note2 = new Notification();
            note2.Subject = "DOOF";
            note2.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHHHHHHHHHH";
            note2.SentDate = DateTime.Now;

            Notification note3 = new Notification();
            note3.Subject = "DOOF";
            note3.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHdgsbdgsfdhHHHHHHHHH";
            note3.SentDate = DateTime.Now;

            notifications.Add(note1);
            notifications.Add(note2);
            notifications.Add(note3);


        }
    }
}
