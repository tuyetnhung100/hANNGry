﻿/*
 * Programmer(s):      Gong-Hao
 * Date:               10/23/2019
 * What the code does: Data access layer of Notification.
 */

using AccountLibrary;
using ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NotificationLibrary
{
    public class NotificationDB
    {
        // Search database by time and message
        public static bool SearchTimeMessage(String input, DateTime start, DateTime end, ref List<Notification> notifications)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
SELECT
  NotificationId,
  Name AS 'SenderName',
  Subject,
  Message,
  SentDate,
  (SELECT
    COUNT(Subscribers_AccountId)
  FROM SubscriberNotification
  WHERE ReceivedNotifications_NotificationId = Notifications.NotificationId)
  AS 'NumberSent'
FROM Notifications
INNER JOIN Accounts
  ON Notifications.SentAccountId = Accounts.AccountId
WHERE SentDate > @Start
AND SentDate < @End
AND Message LIKE '%' + @Message + '%';", connect);

            command.Parameters.AddWithValue("@Message", input);
            command.Parameters.AddWithValue("@Start", start);
            command.Parameters.AddWithValue("@End", end);

            SqlDataReader reader = command.ExecuteReader();

            Notification myNotification;

            int senderNameIndex = reader.GetOrdinal("SenderName");
            int subjectIndex = reader.GetOrdinal("Subject");
            int messageIndex = reader.GetOrdinal("Message");
            int sentDateIndex = reader.GetOrdinal("SentDate");
            int numberSentIndex = reader.GetOrdinal("NumberSent");

            while (reader.Read())
            {
                myNotification = new Notification();
                myNotification.SenderName = reader.GetString(senderNameIndex);
                myNotification.Subject = reader.GetString(subjectIndex);
                myNotification.Message = reader.GetString(messageIndex);
                myNotification.SentDate = reader.GetDateTime(sentDateIndex);
                myNotification.NumberSent = reader.GetInt32(numberSentIndex);
                notifications.Add(myNotification);
            }

            reader.Close();
            connect.Close();
            return true;
        }

        // Search database by message content
        public static bool Search(String input, ref List<Notification> notifications)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
SELECT
  NotificationId,
  Name AS 'SenderName',
  Subject,
  Message,
  SentDate,
  (SELECT
    COUNT(Subscribers_AccountId)
  FROM SubscriberNotification
  WHERE ReceivedNotifications_NotificationId = Notifications.NotificationId)
  AS 'NumberSent'
FROM Notifications
INNER JOIN Accounts
  ON Notifications.SentAccountId = Accounts.AccountId
WHERE Message LIKE '%' + @Message + '%';",
                                                 connect);

            command.Parameters.AddWithValue("@Message", input);

            SqlDataReader reader = command.ExecuteReader();

            Notification myNotification;

            int senderNameIndex = reader.GetOrdinal("SenderName");
            int subjectIndex = reader.GetOrdinal("Subject");
            int messageIndex = reader.GetOrdinal("Message");
            int sentDateIndex = reader.GetOrdinal("SentDate");
            int numberSentIndex = reader.GetOrdinal("NumberSent");

            while (reader.Read())
            {
                myNotification = new Notification();
                myNotification.SenderName = reader.GetString(senderNameIndex);
                myNotification.Subject = reader.GetString(subjectIndex);
                myNotification.Message = reader.GetString(messageIndex);
                myNotification.SentDate = reader.GetDateTime(sentDateIndex);
                myNotification.NumberSent = reader.GetInt32(numberSentIndex);
                notifications.Add(myNotification);
            }

            reader.Close();
            connect.Close();
            return true;
        }

        //Search database by time
        public static bool Load(DateTime start, DateTime end, ref List<Notification> notifications)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
SELECT
  NotificationId,
  Name AS 'SenderName',
  Subject,
  Message,
  SentDate,
  (SELECT
    COUNT(Subscribers_AccountId)
  FROM SubscriberNotification
  WHERE ReceivedNotifications_NotificationId = Notifications.NotificationId)
  AS 'NumberSent'
FROM Notifications
INNER JOIN Accounts
  ON Notifications.SentAccountId = Accounts.AccountId
WHERE SentDate > @Start
AND SentDate < @End;", connect);

            command.Parameters.AddWithValue("@Start", start);
            command.Parameters.AddWithValue("@End", end);

            SqlDataReader reader = command.ExecuteReader();

            Notification myNotification;

            int senderNameIndex = reader.GetOrdinal("SenderName");
            int subjectIndex = reader.GetOrdinal("Subject");
            int messageIndex = reader.GetOrdinal("Message");
            int sentDateIndex = reader.GetOrdinal("SentDate");
            int numberSentIndex = reader.GetOrdinal("NumberSent");

            while (reader.Read())
            {
                myNotification = new Notification();
                myNotification.SenderName = reader.GetString(senderNameIndex);
                myNotification.Subject = reader.GetString(subjectIndex);
                myNotification.Message = reader.GetString(messageIndex);
                myNotification.SentDate = reader.GetDateTime(sentDateIndex);
                myNotification.NumberSent = reader.GetInt32(numberSentIndex);
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
INSERT INTO Notifications
(Subject,
 Message,
 TemplateId,
 SentAccountId,
 SentDate,
 Location)
  VALUES (@Subject,
          @Message,
          @TemplateId,
          @SentAccountId,
          @SentDate,
          @Location);
SELECT
  SCOPE_IDENTITY();";
            command.CommandText = sql;

            // set Parameters
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Subject", notification.Subject);
            command.Parameters.AddWithValue("@Message", notification.Message);
            command.Parameters.AddWithValue("@TemplateId", ParameterHelper.GetNullableValue(notification.TemplateId));
            command.Parameters.AddWithValue("@SentAccountId", notification.SentAccountId);
            command.Parameters.AddWithValue("@SentDate", DateTime.Now);
            command.Parameters.AddWithValue("@Location", notification.Location);

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
INSERT INTO SubscriberNotification
(Subscribers_AccountId,
 ReceivedNotifications_NotificationId)
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
        public static SendNotificationResult SendNotification(Notification notification, ref List<Account> subscribers)
        {
            subscribers.Clear();

            try
            {
                SqlConnection connection = DBConnect.GetConnection();
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // select all subscribers that role is Role.Subscriber
                AccountDB.GetSubscribers(command, notification.Location, ref subscribers);
                if (subscribers.Count == 0)
                {
                    connection.Close();
                    connection.Dispose();
                    command.Dispose();

                    return SendNotificationResult.NoSubscribers;
                }
                // insert Notification and get the new notificationId
                int notificationId = InsertNotification(command, notification);
                notification.NotificationId = notificationId;
                // insert SubscriberNotification map table
                InsertSubscriberNotification(command, notificationId, ref subscribers);

                connection.Close();
                connection.Dispose();
                command.Dispose();

                return SendNotificationResult.Succeeded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return SendNotificationResult.DatabaseError;
            }
        }

        /// <summary>
        /// Update SubscriberNotification after SendCompleted callback
        /// </summary>
        /// <param name="notificationId">The NotificationId</param>
        /// <param name="accountId">The Subscriber's AccountId</param>
        /// <param name="succeeded">Whether the sending email succeeded</param>
        /// <param name="cancelled">Whether the sending email cancelled</param>
        /// <param name="errorMessage">The errorMessage if sending email failed</param>
        /// <returns>Whether the command is succeeded</returns>
        public static bool UpdateSubscriberNotification(
            int notificationId,
            int accountId,
            bool succeeded,
            bool cancelled,
            string errorMessage
        )
        {
            try
            {
                SqlConnection connection = DBConnect.GetConnection();
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                bool failed = !string.IsNullOrWhiteSpace(errorMessage);

                // set Parameters
                command.Parameters.AddWithValue("@NotificationId", notificationId);
                command.Parameters.AddWithValue("@AccountId", accountId);
                command.Parameters.AddWithValue("@Succeeded", succeeded);
                command.Parameters.AddWithValue("@Cancelled", cancelled);
                command.Parameters.AddWithValue("@Failed", failed);
                command.Parameters.AddWithValue("@ErrorMessage", ParameterHelper.GetNullableValue(errorMessage));

                string sql = @"
UPDATE SubscriberNotification
SET Succeeded = @Succeeded,
    Cancelled = @Cancelled,
    Failed = @Failed,
    ErrorMessage = @ErrorMessage
WHERE Subscribers_AccountId = @AccountId
AND ReceivedNotifications_NotificationId = @NotificationId;";

                command.CommandText = sql;
                command.ExecuteNonQuery();

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
