using bopg.api.account.Entities;
using System;

namespace bopg.api.account.Helper
{
    public class UserHelper
    {
        public static BaseHelper Login(Model.Login data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Login();

            try
            {
                var entity = User.Login(data, objJSON);

                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo(entity.SQLDetail);
                retVal.IsError = (entity.ResultCode == 1) ? false : true;
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.Login>(objJSON);
            }

            return retVal;
        }

        public static BaseHelper ValidateSession(Model.Session data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();

            try
            {
                var entity = User.ValidateSession(data, objJSON);

                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo(entity.SQLDetail);
                retVal.IsError = (entity.ResultCode == 1) ? false : true;
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.Session>(objJSON);
            }

            return retVal;
        }

        public static BaseHelper Logout(Model.Session data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();

            try
            {
                var entity = User.Logout(data, objJSON);

                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo(entity.SQLDetail);
                retVal.IsError = (entity.ResultCode == 1) ? false : true;
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.OutputBase>(objJSON);
            }

            return retVal;
        }

        public static BaseHelper UserList(Model.UserList data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();
            var objJSONPage = new Output.User();
            try
            {
                var entity = User.ValidateSession(data, objJSON);
                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo($"sp : {entity.SQLDetail}, elapsed : {entity.SQLElapsed}");

                if (objJSON.ResultCode == 1)
                {
                    data.StampUser = objJSON.Content.Data[0].UserLogin;
                    data.UserID = objJSON.Content.Data[0].UserID;

                    var entityPage = User.UserList(data, objJSONPage);
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSON.ResultCode;
                    objJSONPage.ErrorMessage = objJSON.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.User>(objJSONPage);
            }

            return retVal;
        }

        public static BaseHelper UserAdd(Model.User data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();
            var objJSONPage = new Output.OutputBase();

            try
            {
                var entity = User.ValidateSession(data, objJSON);
                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo($"sp : {entity.SQLDetail}, elapsed : {entity.SQLElapsed}");

                if (objJSON.ResultCode == 1)
                {
                    data.StampUser = objJSON.Content.Data[0].UserLogin;
                    data.UserID = objJSON.Content.Data[0].UserID;

                    var entityPage = User.UserAdd(data, objJSONPage);
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSON.ResultCode;
                    objJSONPage.ErrorMessage = objJSON.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.OutputBase>(objJSONPage);
            }

            return retVal;
        }

        public static BaseHelper UserDelete(Model.User data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();
            var objJSONPage = new Output.OutputBase();

            try
            {
                var entity = User.ValidateSession(data, objJSON);
                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo($"sp : {entity.SQLDetail}, elapsed : {entity.SQLElapsed}");

                if (objJSON.ResultCode == 1)
                {
                    data.StampUser = objJSON.Content.Data[0].UserLogin;
                    

                    var entityPage = User.UserDelete(data, objJSONPage);
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSON.ResultCode;
                    objJSONPage.ErrorMessage = objJSON.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.OutputBase>(objJSONPage);
            }

            return retVal;
        }

        public static BaseHelper UserChangePassword(Model.Password data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();
            var objJSONPage = new Output.OutputBase();

            try
            {
                var entity = User.ValidateSession(data, objJSON);
                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo($"sp : {entity.SQLDetail}, elapsed : {entity.SQLElapsed}");

                if (objJSON.ResultCode == 1)
                {
                    data.StampUser = objJSON.Content.Data[0].UserLogin;
                    data.UserID = objJSON.Content.Data[0].UserID;

                    var entityPage = User.UpdateChangePassword(data, objJSONPage);
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSON.ResultCode;
                    objJSONPage.ErrorMessage = objJSON.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.OutputBase>(objJSONPage);
            }

            return retVal;
        }
    }
}
