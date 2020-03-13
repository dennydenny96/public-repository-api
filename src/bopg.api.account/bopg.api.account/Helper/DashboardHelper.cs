using bopg.api.account.Entities;
using System;

namespace bopg.api.account.Helper
{
    public class DashboardHelper
    {
        public static BaseHelper MenuGet(Model.Session data)
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.Session();
            var objJSONMenu = new Output.Menu();

            try
            {
                var entity = User.ValidateSession(data, objJSON);
                retVal.SQLElapsed = entity.SQLElapsed;
                retVal.SQLInfo($"sp : {entity.SQLDetail}, elapsed : {entity.SQLElapsed}");

                if(objJSON.ResultCode == 1)
                {
                    var dataMenu = new Model.User
                    {
                        UserID = objJSON.Content.Data[0].UserID,
                        StampUser = objJSON.Content.Data[0].UserLogin
                    };

                    var entityMenu = Dashboard.MenuGet(dataMenu, objJSONMenu);
                    retVal.SQLElapsed += entityMenu.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityMenu.SQLDetail}, elapsed : {entityMenu.SQLElapsed}");

                    retVal.IsError = (entityMenu.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONMenu.ResultCode = objJSON.ResultCode;
                    objJSONMenu.ErrorMessage = objJSON.ErrorMessage;
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
                retVal.SerializeObject<Output.Menu>(objJSONMenu);
            }

            return retVal;
        }
    }
}
