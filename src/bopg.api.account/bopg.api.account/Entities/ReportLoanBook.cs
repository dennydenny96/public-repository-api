using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class ReportLoanBook
    {
        public static BasicEntity ReportLoanBookList(Model.ReportLoanBookList data, Output.ReportLoanBookList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@level", data.Level);
            retVal.AddParameter("@userlogin", data.UserLogin);
            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);

            data.SqlDetail = retVal.SQLCommandBuilder("spReportLoanBookList");

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
                        var item = new Output.ReportLoanBookListData();

                        item.UserName = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.Title = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.Price = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.LoanDay = (reader.IsDBNull(3)) ? DateTime.MinValue : reader.GetDateTime(3);
                        item.ReturnDay = (reader.IsDBNull(4)) ? DateTime.MinValue : reader.GetDateTime(4);
                        
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
