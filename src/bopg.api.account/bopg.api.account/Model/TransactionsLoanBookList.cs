using System;

namespace bopg.api.account.Model
{
    public class TransactionsLoanBookList : List
    {
        public string Level { get; set; }
        public string UserLogin { get; set; }
        public int BookID { get; set; }

    }
}
