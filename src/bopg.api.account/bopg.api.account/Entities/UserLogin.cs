using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class UserLogin
    {
        public static BasicEntity Registration(Model.UserLoginAdd data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@mobileNumber", data.MobileNumber);
            retVal.AddParameter("@firstName", data.FirstName);
            retVal.AddParameter("@lastName", data.LastName);
            retVal.AddParameter("@dateOfBirth", data.DateOfBirth);
            retVal.AddParameter("@gender", data.Gender);
            retVal.AddParameter("@email", data.Email);

            data.SqlDetail = retVal.SQLCommandBuilder("spRegistration");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity CreateAccountLoginUser(Model.UserLoginAdd data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@userlogin", data.UserLogin);
            retVal.AddParameter("@password", data.Password);
            retVal.AddParameter("@level", data.Level);
            retVal.AddParameter("@mobileNumber", data.MobileNumber);

            data.SqlDetail = retVal.SQLCommandBuilder("spCreateAccountLoginUser");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity UserList(Model.UserList data, Output.UserList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            retVal.AddParameter("@level", data.Level);
            data.SqlDetail = retVal.SQLCommandBuilder("spLoginUserList");

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
                        var item = new Output.UserListData();

                        item.UserLogin = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.Level = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        
                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity UserLoginAdd(Model.UserLoginAdd data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@userlogin", data.UserLogin);
            retVal.AddParameter("@password", data.Password);
            retVal.AddParameter("@level", data.Level);

            data.SqlDetail = retVal.SQLCommandBuilder("spAddLoginUser");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

    }
}
