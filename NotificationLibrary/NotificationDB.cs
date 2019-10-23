using AccountLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NotificationLibrary
{
    public class NotificationDB
    {
        // It will be removed when DBConnect project is merged.
        private static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
        }

        public static bool Load(ref List<Notification> notifications)
        {
            return true;
        }

        /// <summary>
        /// Insert Notification
        /// </summary>
        /// <param name="command">SqlCommand</param>
        /// <param name="notification">Notification data</param>
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
        /// Get Subscriber Id list
        /// </summary>
        /// <param name="command">SqlCommand</param>
        /// <param name="subscribers"></param>
        private static void GetSubscribers(SqlCommand command, ref List<Account> subscribers)
        {
            subscribers.Clear();

            // set select sql
            string sql = @"
SELECT
  AccountId,
  Name
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

            while (reader.Read())
            {
                Account account = new Account
                {
                    AccountId = reader.GetInt32(accountId),
                    Name = reader.GetString(name)
                };
                subscribers.Add(account);
            }

            reader.Close();
        }

        /// <summary>
        /// Insert SubscriberNotification map table and reference subscriberIds
        /// </summary>
        /// <param name="command">SqlCommand</param>
        /// <param name="notificationId">Notification Id</param>
        /// <param name="subscribers">Subscriber list</param>
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
        /// Send Notification and reference subscriberIds
        /// </summary>
        /// <param name="notification">Notification data</param>
        /// <param name="subscribers">Subscriber list</param>
        /// <returns>whether the command is succeeded</returns>
        public static bool SendNotification(Notification notification, ref List<Account> subscribers)
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // insert Notification and get the new notificationId
                int notificationId = InsertNotification(command, notification);
                // select all subscribers that role is Role.Subscriber
                GetSubscribers(command, ref subscribers);
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
