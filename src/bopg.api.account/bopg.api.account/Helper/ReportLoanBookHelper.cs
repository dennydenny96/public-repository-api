using bopg.api.account.Entities;
using System;

namespace bopg.api.account.Helper
{
    public class ReportLoanBookHelper
    {
        public static BaseHelper ReportLoanBookList(Model.List data)
        {
            var retVal = new BaseHelper();
            var objJSONPage = new Output.ReportLoanBookList();
            try
            {
                var entityPage = ReportLoanBook.ReportLoanBookList(data, objJSONPage);
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
                retVal.SerializeObject<Output.ReportLoanBookList>(objJSONPage);
            }

            return retVal;
        }
    }
}
