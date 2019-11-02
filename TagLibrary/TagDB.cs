/*
* Programmer(s):      Gong-Hao
* Date:               10/23/2019
* What the code does: Data access layer of Tag.
*/

using ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TagLibrary
{
    public class TagDB
    {
        // Adds values to Tag table for Type and Name.
        public static bool Add(Tag myTag)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Insert into Tags(Type, Name)" +
                " Values(1, @Name)", connect);

            command.Parameters.AddWithValue("@Type", myTag.Type);
            command.Parameters.AddWithValue("@Name", myTag.Name);

            command.ExecuteNonQuery();

            connect.Close();
            return true;
        }

        /// <summary>
        /// Load all tags.
        /// </summary>
        /// <param name="tags">The result list of tags</param>
        /// <returns>Whether the command is succeeded</returns>
        public static bool Load(ref List<Tag> tags)
        {
            tags.Clear();

            try
            {
                SqlConnection connection = DBConnect.GetConnection();
                connection.Open();
                string sql = @"
SELECT
  TagId,
  Type,
  Name
FROM Tags;";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                int tagId = reader.GetOrdinal("TagId");
                int type = reader.GetOrdinal("Type");
                int name = reader.GetOrdinal("Name");

                while (reader.Read())
                {
                    Tag tag = new Tag
                    {
                        TagId = reader.GetInt32(tagId),
                        Type = (TagType)reader.GetInt32(type),
                        Name = reader.GetString(name)
                    };
                    tags.Add(tag);
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
