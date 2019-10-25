/*
 * Programmer(s):      Gong-Hao
 * Date:               10/23/2019
 * What the code does: Data access layer of Tag.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TagLibrary
{
    public class TagDB
    {
        // It will be removed when DBConnect project is merged.
        private static SqlConnection GetConnection()
        {
            SqlConnection connect = new SqlConnection("Server=cisdbss.pcc.edu;Database=234a_hANNGry;User Id=234a_hANNGry;Password = RUSerious?; ");
            return connect;
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
                SqlConnection connection = GetConnection();
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

        /// <summary>
        /// Load tags by templateId.
        /// </summary>
        /// <param name="tags">The result list of tags</param>
        /// <param name="templateId">templateId</param>
        /// <returns>Whether the command is succeeded</returns>
        public static bool LoadByTemplateId(ref List<Tag> tags, int templateId)
        {
            tags.Clear();

            try
            {
                SqlConnection connection = GetConnection();
                connection.Open();
                string sql = @"
SELECT
  TagId,
  Type,
  Name
FROM Tags
WHERE TagId IN (SELECT
  Tags_TagId
FROM dbo.TagTemplate
WHERE Templates_TemplateId = @TemplateId);";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@TemplateId", templateId);
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

        // fake version of LoadByTemplateId in case the DB data is messed up
        public static bool FakeLoadByTemplateId(ref List<Tag> tags, int templateId)
        {
            tags.Clear();
            tags.Add(new Tag
            {
                TagId = 1,
                Type = TagType.DatabaseField,
                Name = "Student Name"
            });
            tags.Add(new Tag
            {
                TagId = 2,
                Type = TagType.DatabaseField,
                Name = "Staff Name"
            });
            tags.Add(new Tag
            {
                TagId = 3,
                Type = TagType.UserInput,
                Name = "Food"
            });
            tags.Add(new Tag
            {
                TagId = 4,
                Type = TagType.UserInput,
                Name = "Campus Name"
            });
            tags.Add(new Tag
            {
                TagId = 5,
                Type = TagType.UserInput,
                Name = "Room"
            });
            tags.Add(new Tag
            {
                TagId = 6,
                Type = TagType.UserInput,
                Name = "Date"
            });
            tags.Add(new Tag
            {
                TagId = 7,
                Type = TagType.UserInput,
                Name = "Start Time"
            });
            tags.Add(new Tag
            {
                TagId = 8,
                Type = TagType.UserInput,
                Name = "End Time"
            });
            return true;
        }
    }
}
