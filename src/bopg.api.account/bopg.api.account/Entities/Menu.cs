using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class Menu
    {
        public static BasicEntity MenuList(Model.MenuList data, Output.MenuList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            retVal.AddParameter("@first_name", data.FirstName);
            retVal.AddParameter("@last_name", data.LastName);
            retVal.AddParameter("@email", data.Email);
            retVal.AddParameter("@department_id", data.DepartmentID);
            retVal.AddParameter("@jobtitle_id", data.JobTitleID);

            data.SqlDetail = retVal.SQLCommandBuilder("spMenuList");

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
                        var item = new Output.MenuListData();

                        item.ID = (reader.IsDBNull(0)) ? 0 : reader.GetInt64(0);
                        item.FirstName = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.LastName = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.Email = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);
                        item.DepartmentName = (reader.IsDBNull(4)) ? string.Empty : reader.GetString(4);
                        item.JobTitleName = (reader.IsDBNull(5)) ? string.Empty : reader.GetString(5);
                        item.HireDate = (reader.IsDBNull(6)) ? DateTime.MinValue : reader.GetDateTime(6);
                        item.Gender = (reader.IsDBNull(7)) ? string.Empty : reader.GetString(7);
                        item.PlaceOfBirth = (reader.IsDBNull(8)) ? string.Empty : reader.GetString(8);
                        item.DateOfBirth = (reader.IsDBNull(9)) ? DateTime.MinValue : reader.GetDateTime(9);
                        item.Address = (reader.IsDBNull(10)) ? string.Empty : reader.GetString(10);
                        item.Phone = (reader.IsDBNull(11)) ? string.Empty : reader.GetString(11);
                        item.NIK = (reader.IsDBNull(12)) ? string.Empty : reader.GetString(12);
                        item.DepartmentID = (reader.IsDBNull(13)) ? 0 : reader.GetInt32(13);
                        item.JobTitleID = (reader.IsDBNull(14)) ? 0 : reader.GetInt32(14);

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
