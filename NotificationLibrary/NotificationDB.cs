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

            SqlCommand command = new SqlCommand(@"
SELECT
  Name AS 'SenderName',
  Subject,
  Message,
  SentDate
FROM Notifications
INNER JOIN Accounts
  ON Notifications.SentAccountId = Accounts.AccountId
WHERE SentDate > @start
AND SentDate < @end", connect);

            command.Parameters.AddWithValue("@start", start);
            command.Parameters.AddWithValue("@end", end);

            SqlDataReader reader = command.ExecuteReader();

            Notification myNotification;

            int senderName = reader.GetOrdinal("SenderName");
            int subject = reader.GetOrdinal("Subject");
            int message = reader.GetOrdinal("Message");
            int sentDate = reader.GetOrdinal("SentDate");

            while (reader.Read())
            {
                myNotification = new Notification();
                myNotification.Subject = reader.GetString(subject);
                myNotification.Message = reader.GetString(message);
                myNotification.SenderName=reader.GetString(senderName);
                myNotification.SentDate = reader.GetDateTime(sentDate);
                notifications.Add(myNotification);
            }

            reader.Close();

            connect.Close();

            
            return true;
        }
        //private static void LoadFakeNotifications(ref List<Notification> notifications)
        //{
        //    Notification note1 = new Notification();
        //    note1.Subject = "Food";
        //    note1.Message = "BLAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" + Environment.NewLine + "BLAAAHHHHHHHHHHHHHHHHAAH";
        //    note1.SentDate = DateTime.Now;

        //    Notification note2 = new Notification();
        //    note2.Subject = "DOOF";
        //    note2.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHHHHHHHHHH";
        //    note2.SentDate = DateTime.Now;

        //    Notification note3 = new Notification();
        //    note3.Subject = "DOOF";
        //    note3.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHdgsbdgsfdhHHHHHHHHH";
        //    note3.SentDate = DateTime.Now;

        //    notifications.Add(note1);
        //    notifications.Add(note2);
        //    notifications.Add(note3);


        //}
    }
}
