using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class TransactionsLoanBookList : OutputBase
    {
        public TransactionsLoanBookListContent Content { get; set; }

        public TransactionsLoanBookList()
        {
            this.Content = new TransactionsLoanBookListContent();
        }
    }

    public class TransactionsLoanBookListContent
    {
        public Int32 TotalRows { get; set; }
        public List<TransactionsLoanBookListData> Data { get; set; }

        public TransactionsLoanBookListContent()
        {
            this.Data = new List<TransactionsLoanBookListData>();
        }
    }

    public class TransactionsLoanBookListData
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public DateTime LoanDay { get; set; }
        public DateTime ReturnDay { get; set; }
        public int BookID { get; set; }
    }
}
