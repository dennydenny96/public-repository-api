using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class GetUser
    {
        public static BasicEntity GetUserList(Model.GetUserList data, Output.GetUserList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            data.SqlDetail = retVal.SQLCommandBuilder("spGetUserList");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                if (retVal.ResultCode == 1)
                {
                    reader.NextResult();
                    while (reader.Read())
                    {
                        obj.Content.TotalRows = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    }

                        reader.NextResult();
                    while (reader.Read())
                    {
                        var item = new Output.GetUserListData();

                        item.Name = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.Age = (reader.IsDBNull(1)) ? 0 : reader.GetInt32(1);
                        item.City = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        
                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }
    }
}
