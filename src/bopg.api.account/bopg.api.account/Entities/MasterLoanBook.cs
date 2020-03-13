using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class MasterLoanBook
    {
        public static BasicEntity MasterLoanBookList(Model.TransactionsLoanBookList data, Output.MasterLoanBookList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@page", data.Page);
            retVal.AddParameter("@page_size", data.PageSize);
            retVal.AddParameter("@level", data.Level);

            data.SqlDetail = retVal.SQLCommandBuilder("spMasterDataBookList");

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
                        var item = new Output.MasterLoanBookListData();

                        item.Title = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.Price = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.Borrowed = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        
                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity MasterLoanBookAdd(Model.MasterLoanBookAdd data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@title", data.Title);
            retVal.AddParameter("@price", data.Price);

            data.SqlDetail = retVal.SQLCommandBuilder("spMasterDataBookAdd");

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
