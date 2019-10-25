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

            SqlCommand command = new SqlCommand("Insert into Template(Name, Message, CreatedAccountId, CreatedDate)" +
                " Values(@Name, @Message, @CreatedAccountId, @CreatedDate)", connect);

            command.Parameters.AddWithValue("@Name", myTemplate.Name);
            command.Parameters.AddWithValue("@Message", myTemplate.Message);
            command.Parameters.AddWithValue("@CreatedAccountId", myTemplate.CreatedAccountId);
            command.Parameters.AddWithValue("@CreatedDate", myTemplate.CreatedDate);

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
  Templates.Name,
  Templates.Message,
  Accounts.Name AS CreatedAccountName,
  Templates.CreatedDate
FROM Templates
INNER JOIN Accounts
  ON Accounts.AccountId = Templates.CreatedAccountId;";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                int templateId = reader.GetOrdinal("TemplateId");
                int name = reader.GetOrdinal("Name");
                int message = reader.GetOrdinal("Message");
                int createdAccountName = reader.GetOrdinal("CreatedAccountName");
                int createdDate = reader.GetOrdinal("CreatedDate");

                while (reader.Read())
                {
                    Template template = new Template
                    {
                        TemplateId = reader.GetInt32(templateId),
                        Name = reader.GetString(name),
                        Message = reader.GetString(message),
                        CreatedAccountName = reader.GetString(createdAccountName),
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
