using System;

namespace bopg.api.account.Model
{
    public class GetUserList : List
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
