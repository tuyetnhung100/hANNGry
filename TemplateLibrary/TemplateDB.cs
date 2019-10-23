using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConnectDB;

namespace TemplateLibrary
{
    public class TemplateDB
    {
        // Connects to the Template db and loads the attributes for each template.
        public static Boolean Load(ref List<Template> template)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select Name, Message, CreatedAccountId, CreatedDate from Template", connect);

            SqlDataReader reader = command.ExecuteReader();
            Template myTemplate;

            while (reader.Read())
            {
                myTemplate = new Template();
                myTemplate.Name = reader.GetString(0);
                myTemplate.Message = reader.GetString(1);
                myTemplate.CreatedAccountId = reader.GetInt32(2);
                myTemplate.CreatedDate = reader.GetDateTime(3);
                template.Add(myTemplate);
            }

            reader.Close();

            connect.Close();


            return true;
        }

        // Adds values to Template table.
        public static Boolean Add(Template myTemplate)
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
    }
}
