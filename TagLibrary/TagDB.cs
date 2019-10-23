using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConnectDB;

namespace TagLibrary
{
    public class TagDB
    {
        // Connects to the Tag db and loads the Type and Name for each tag.
        public static Boolean Load(ref List<Tag> tags)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Select TagId, Name from Tags", connect);

            SqlDataReader reader = command.ExecuteReader();
            Tag myTag;

            while (reader.Read())
            {
                myTag = new Tag();
                myTag.Type = (TagType)reader.GetInt32(0);
                myTag.Name = reader.GetString(1);
                tags.Add(myTag);
            }

            reader.Close();

            connect.Close();


            return true;
        }

        // Adds values to Tag table for Type and Name.
        public static Boolean Add(Tag myTag)
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
    }
}
