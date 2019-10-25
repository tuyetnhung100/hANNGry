/*
 * Programmer(s):      Gong-Hao
 * Date:               10/23/2019
 * What the code does: Data access layer of Notification.
 */

using AccountLibrary;
using System;
using System.Collections.Generic;


using ConnectDB;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace NotificationLibrary
{
    public class NotificationDB
    {

        public static Boolean Load(DateTime start, DateTime end, ref List<Notification> notifications)

        {
            SqlConnection connect = DBConnect.GetConnection();
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


        /// <summary>
        /// Insert a Notification.
        /// </summary>
        /// <param name="command">The SqlCommand</param>
        /// <param name="notification">The Notification data</param>
        /// <returns></returns>
        private static int InsertNotification(SqlCommand command, Notification notification)
        {
            // set insert sql
            string sql = @"
INSERT INTO Notifications (Subject
, Message
, TemplateId
, SentAccountId
, SentDate)
  VALUES (@Subject, @Message, @TemplateId, @SentAccountId, @SentDate);
SELECT
  SCOPE_IDENTITY();";
            command.CommandText = sql;

            // set Parameters
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Subject", notification.Subject);
            command.Parameters.AddWithValue("@Message", notification.Message);
            if (notification.TemplateId == null)
            {
                command.Parameters.AddWithValue("@TemplateId", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@TemplateId", notification.TemplateId);
            }
            command.Parameters.AddWithValue("@SentAccountId", notification.SentAccountId);
            command.Parameters.AddWithValue("@SentDate", DateTime.Now);

            // retuen notificationId by SCOPE_IDENTITY
            int notificationId = Convert.ToInt32(command.ExecuteScalar());
            return notificationId;
        }

        /// <summary>
        /// Insert SubscriberNotification map table and reference subscribers.
        /// </summary>
        /// <param name="command">The SqlCommand</param>
        /// <param name="notificationId">The NotificationId</param>
        /// <param name="subscribers">The Subscribers</param>
        private static void InsertSubscriberNotification(SqlCommand command, int notificationId, ref List<Account> subscribers)
        {
            List<string> values = new List<string>();

            // set Parameters
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@NotificationId", notificationId);

            // use a for loop to set parameters
            for (int i = 0; i < subscribers.Count; i++)
            {
                // use indexs to set param names instead of directly string concatenation
                // to prevent sql injection
                Account subscriber = subscribers[i];
                string accountIdParam = "@AccountId_" + i;
                string value = "(" + accountIdParam + ", @NotificationId)";
                command.Parameters.AddWithValue(accountIdParam, subscriber.AccountId);
                values.Add(value);
            }

            // it will be something like:
            // INSERT INTO Table
            //   (AccountId, NotificationId)
            // VALUES
            //   (@AccountId_0, @NotificationId),
            //   (@AccountId_1, @NotificationId),
            //   (@AccountId_2, @NotificationId)
            string sql = @"
INSERT INTO SubscriberNotification (Subscribers_AccountId
, ReceivedNotifications_NotificationId)
  VALUES " + string.Join("," + Environment.NewLine, values);

            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Send Notification and reference subscribers.
        /// </summary>
        /// <param name="notification">The Notification data</param>
        /// <param name="subscribers">The Subscribers</param>
        /// <returns>Whether the command is succeeded</returns>
        public static bool SendNotification(Notification notification, ref List<Account> subscribers)
        {
            try
            {
                SqlConnection connection = DBConnect.GetConnection();
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // insert Notification and get the new notificationId
                int notificationId = InsertNotification(command, notification);
                // select all subscribers that role is Role.Subscriber
                AccountDB.GetSubscribers(command, ref subscribers);
                // insert SubscriberNotification map table
                InsertSubscriberNotification(command, notificationId, ref subscribers);

                connection.Close();
                connection.Dispose();
                command.Dispose();

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
