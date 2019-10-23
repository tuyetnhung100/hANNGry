using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConnectDB;

namespace TagLibrary
{
    public class TagDB
    {
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
                myTag.TagId = reader.GetInt32(0);
                myTag.Name = reader.GetString(1);
                tags.Add(myTag);
            }

            reader.Close();

            connect.Close();


            return true;
        }

        public static Boolean Add(Tag myTag)
        {
            SqlConnection connect = DBConnect.GetConnection();
            connect.Open();

            SqlCommand command = new SqlCommand("Insert into Tags(TagId, Name)" +
                " Values('', @Name)", connect);

            command.Parameters.AddWithValue("@TagId", myTag.TagId);
            command.Parameters.AddWithValue("@Name", myTag.Name);

            command.ExecuteNonQuery();


            connect.Close();
            return true;
        }
    }
}
