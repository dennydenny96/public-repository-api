using bopg.api.account.Entities;
using System;

namespace bopg.api.account.Helper
{
    public class ComboHelper
    {
        public static BaseHelper ComboDepartment(Model.List data)
        {
            var retVal = new BaseHelper();
            var objJSONPage = new Output.Combo();
            try
            {
                var entityPage = Combo.ComboDepartmentName(data, objJSONPage);
                if (objJSONPage.ResultCode == 1)
                {
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSONPage.ResultCode;
                    objJSONPage.ErrorMessage = objJSONPage.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSONPage.ResultCode = 69998;
                    objJSONPage.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSONPage.ResultCode = 69999;
                    objJSONPage.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.Combo>(objJSONPage);
            }

            return retVal;
        }
        public static BaseHelper ComboJobTitle(Model.ComboJobTitle data)
        {
            var retVal = new BaseHelper();
            var objJSONPage = new Output.Combo();
            try
            {
                var entityPage = Combo.ComboJobTitle(data, objJSONPage);
                if (objJSONPage.ResultCode == 1)
                {
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSONPage.ResultCode;
                    objJSONPage.ErrorMessage = objJSONPage.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSONPage.ResultCode = 69998;
                    objJSONPage.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSONPage.ResultCode = 69999;
                    objJSONPage.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.Combo>(objJSONPage);
            }

            return retVal;
        }

        public static BaseHelper ComboDataBook(Model.List data)
        {
            var retVal = new BaseHelper();
            var objJSONPage = new Output.ComboDataBook();
            try
            {
                var entityPage = Combo.ComboDataBook(data, objJSONPage);
                if (objJSONPage.ResultCode == 1)
                {
                    retVal.SQLElapsed += entityPage.SQLElapsed;
                    retVal.SQLInfo($"sp : {entityPage.SQLDetail}, elapsed : {entityPage.SQLElapsed}");

                    retVal.IsError = (entityPage.ResultCode == 1) ? false : true;
                }
                else
                {
                    objJSONPage.ResultCode = objJSONPage.ResultCode;
                    objJSONPage.ErrorMessage = objJSONPage.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}, detail:{data.SqlDetail}");
                    retVal.SQLException = true;

                    objJSONPage.ResultCode = 69998;
                    objJSONPage.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSONPage.ResultCode = 69999;
                    objJSONPage.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.ComboDataBook>(objJSONPage);
            }

            return retVal;
        }

    }
}
