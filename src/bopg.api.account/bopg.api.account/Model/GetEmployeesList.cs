using System;

namespace bopg.api.account.Model
{
    public class GetEmployeesList : List
    {
        public Int64 ID { get; set; }
        public string NIK { get; set; }
    }
}
