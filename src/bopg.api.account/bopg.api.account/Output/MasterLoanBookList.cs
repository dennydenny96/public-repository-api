using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class MasterLoanBookList : OutputBase
    {
        public MasterLoanBookListContent Content { get; set; }

        public MasterLoanBookList()
        {
            this.Content = new MasterLoanBookListContent();
        }
    }

    public class MasterLoanBookListContent
    {
        public Int32 TotalRows { get; set; }
        public List<MasterLoanBookListData> Data { get; set; }

        public MasterLoanBookListContent()
        {
            this.Data = new List<MasterLoanBookListData>();
        }
    }

    public class MasterLoanBookListData
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public string Borrowed { get; set; }

    }
}
