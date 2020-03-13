using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class TransactionsLoanBook
    {
        public static BasicEntity TransactionsLoanBookList(Model.TransactionsLoanBookList data, Output.TransactionsLoanBookList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            retVal.AddParameter("@level", data.Level);
            retVal.AddParameter("@userlogin", data.UserLogin);

            data.SqlDetail = retVal.SQLCommandBuilder("spTransactionsLoanBookList");

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
                        var item = new Output.TransactionsLoanBookListData();

                        item.Title = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.Price = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.LoanDay = (reader.IsDBNull(2)) ? DateTime.MinValue : reader.GetDateTime(2);
                        item.ReturnDay = (reader.IsDBNull(3)) ? DateTime.MinValue : reader.GetDateTime(3);
                        item.BookID = (reader.IsDBNull(4)) ? 0 : reader.GetInt32(4);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity TransactionLoanBookBorrow(Model.TransactionsLoanBookList data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@username", data.UserLogin);
            retVal.AddParameter("@bookID", data.BookID);
            
            data.SqlDetail = retVal.SQLCommandBuilder("spLoanBookAdd");

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

        public static BasicEntity TransactionLoanBookReturn(Model.TransactionsLoanBookList data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@username", data.UserLogin);
            retVal.AddParameter("@bookID", data.BookID);

            data.SqlDetail = retVal.SQLCommandBuilder("spReturnBook");

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
