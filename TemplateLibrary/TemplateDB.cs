/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data access layer of Template.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TemplateLibrary
{
    public class TemplateDB
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
        }

        /// <summary>
        /// Load all templates.
        /// </summary>
        /// <param name="templates">result list of templates</param>
        /// <returns></returns>
        public static Boolean Load(ref List<Template> templates)
        {
            try
            {
                SqlConnection connection = TemplateDB.GetConnection();
                connection.Open();
                string sql = @"
SELECT [Templates].[TemplateId], 
       [Templates].[Name], 
       [Templates].[Message], 
       [Accounts].[Name] AS 'CreatedAccountName', 
       [Templates].[CreatedDate]
FROM[Templates]
       LEFT JOIN[Accounts] ON[Accounts].[AccountId] = [Templates].[CreatedAccountId]; ; ";
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
