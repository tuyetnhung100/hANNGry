/*
 * Programmer(s):      Gong-Hao
 * Date:               10/23/2019
 * What the code does: Data access layer of Template.
 */

using ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TemplateLibrary
{
    public class TemplateDB
    {
        // Adds values to Template table.
        public static bool Add(Template myTemplate)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
INSERT INTO Templates
(Subject,
 Message,
 CreatedAccountId,
 CreatedDate)
  VALUES (@Subject,
          @Message,
          @CreatedAccountId,
          @CreatedDate);", connect);

            command.Parameters.AddWithValue("@Subject", myTemplate.Subject);
            command.Parameters.AddWithValue("@Message", myTemplate.Message);
            command.Parameters.AddWithValue("@CreatedAccountId", myTemplate.CreatedAccountId);
            command.Parameters.AddWithValue("@CreatedDate", myTemplate.CreatedDate);

            command.ExecuteNonQuery();

            connect.Close();
            return true;
        }

        // Updates values in Template table.
        public static bool Update(Template myTemplate)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
UPDATE Templates
SET Message = @Message,
    CreatedAccountId = @CreatedAccountId,
    CreatedDate = @CreatedDate
WHERE Subject = @Subject;", connect);

            command.Parameters.AddWithValue("@Subject", myTemplate.Subject);
            command.Parameters.AddWithValue("@Message", myTemplate.Message);
            command.Parameters.AddWithValue("@CreatedAccountId", myTemplate.CreatedAccountId);
            command.Parameters.AddWithValue("@CreatedDate", myTemplate.CreatedDate);

            command.ExecuteNonQuery();

            connect.Close();
            return true;
        }

        // Deletes values in Template table.
        public static bool Delete(Template myTemplate)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand(@"
            DELETE FROM Templates
            WHERE Subject = @Subject;", connect);

            command.Parameters.AddWithValue("@Subject", myTemplate.Subject);

            command.ExecuteNonQuery();

            connect.Close();
            return true;
        }

        /// <summary>
        /// Load all templates.
        /// </summary>
        /// <param name="templates">The result list of templates</param>
        /// <returns>Whether the command is succeeded</returns>
        public static bool Load(ref List<Template> templates)
        {
            templates.Clear();

            try
            {
                SqlConnection connection = DBConnect.GetConnection();
                connection.Open();
                string sql = @"
                SELECT
                Templates.TemplateId,
                Templates.Subject,
                Templates.Message,
                Accounts.Name AS CreatedEmployeeName,
                Templates.CreatedDate
                FROM Templates
                INNER JOIN Accounts
                ON Accounts.AccountId = Templates.CreatedAccountId;";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                int templateId = reader.GetOrdinal("TemplateId");
                int subject = reader.GetOrdinal("Subject");
                int message = reader.GetOrdinal("Message");
                int createdEmployeeName = reader.GetOrdinal("CreatedEmployeeName");
                int createdDate = reader.GetOrdinal("CreatedDate");

                while (reader.Read())
                {
                    Template template = new Template
                    {
                        TemplateId = reader.GetInt32(templateId),
                        Subject = reader.GetString(subject),
                        Message = reader.GetString(message),
                        CreatedEmployeeName = reader.GetString(createdEmployeeName),
                        CreatedDate = reader.GetDateTime(createdDate)
                    };
                    templates.Add(template);
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
    }
}
