using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class User
    {
        public static BasicEntity Login(Model.Login data, Output.Login obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@Password", data.Password);
            retVal.AddParameter("@UserLogin", data.UserLogin);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserLogin");

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
                        var item = new Output.LoginData();

                        item.UserID = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.SessionToken = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.Level = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);

                        obj.Content.Data.Add(item);
                    }
                }


                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity ValidateSession(Model.Session data, Output.Session obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@user_login", data.UserLogin);
            retVal.AddParameter("@session_token", data.SessionToken);
            retVal.AddParameter("@origin","W");
            data.SqlDetail = retVal.SQLCommandBuilder("spUserValidateSession");

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
                        var item = new Output.SessionData();

                        item.UserID = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                        item.SessionToken = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.UserGroup = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.isFirstLogin = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);
                        item.CompanyID = (reader.IsDBNull(4)) ? string.Empty : reader.GetString(4);
                        item.OperatorID = (reader.IsDBNull(5)) ? string.Empty : reader.GetString(5);
                        item.UserGuid = (reader.IsDBNull(6)) ? string.Empty : reader.GetString(6);
                        item.AuthKey = (reader.IsDBNull(8)) ? string.Empty : reader.GetString(8);
                        item.UserLogin = (reader.IsDBNull(9)) ? string.Empty : reader.GetString(9);
                        
                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity Logout(Model.Session data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@session_token", data.SessionToken);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserLogout");

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

        public static BasicEntity UserList(Model.UserList data, Output.User obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@user_login", data.Username);
            retVal.AddParameter("@user_group_name", data.UserGroupName);
            retVal.AddParameter("@user_id", data.UserID);
            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserList");

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
                        var item = new Output.UserData();

                        item.UserID = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                        item.UserGroupName = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.UserLogin = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.UserActive = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);
                        item.UserSuspend = (reader.IsDBNull(4)) ? string.Empty : reader.GetString(4);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity UserAdd(Model.User data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@level_id", data.LevelAdd);
            retVal.AddParameter("@CompanyID", data.CompanyAdd);
            retVal.AddParameter("@OperatorID", data.OperatorAdd);
            retVal.AddParameter("@UserLogin", data.UserLoginAdd);
            retVal.AddParameter("@Password", data.PasswordAdd);
            retVal.AddParameter("@UserName", data.UserNameAdd);
            retVal.AddParameter("@Email", data.EmailAdd);
            retVal.AddParameter("@authorization_id", data.AuthorizationID);
            retVal.AddParameter("@user_id", data.UserID);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserAdd");

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

        public static BasicEntity UserDelete(Model.User data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

      
            retVal.AddParameter("@authorization_id", data.AuthorizationID);
            retVal.AddParameter("@user_id", data.UserID);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserDelete");

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

        public static BasicEntity UpdateChangePassword(Model.Password data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@UserID", data.UserID);
            retVal.AddParameter("@OldPassword", data.OldPassword);
            retVal.AddParameter("@NewPassword", data.NewPassword);
            data.SqlDetail = retVal.SQLCommandBuilder("spUserChangePassword");

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
