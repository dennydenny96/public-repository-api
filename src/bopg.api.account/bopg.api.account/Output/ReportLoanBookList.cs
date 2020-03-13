using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class ReportLoanBookList : OutputBase
    {
        public ReportLoanBookListContent Content { get; set; }

        public ReportLoanBookList()
        {
            this.Content = new ReportLoanBookListContent();
        }
    }

    public class ReportLoanBookListContent
    {
        public Int32 TotalRows { get; set; }
        public List<ReportLoanBookListData> Data { get; set; }

        public ReportLoanBookListContent()
        {
            this.Data = new List<ReportLoanBookListData>();
        }
    }

    public class ReportLoanBookListData
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public DateTime LoanDay { get; set; }
        public DateTime ReturnDay { get; set; }
    }
}
