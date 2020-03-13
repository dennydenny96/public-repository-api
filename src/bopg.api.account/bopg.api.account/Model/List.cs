using System;

namespace bopg.api.account.Model
{
    public class List : Session
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
    }
}
