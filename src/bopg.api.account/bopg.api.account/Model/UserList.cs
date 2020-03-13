using System;

namespace bopg.api.account.Model
{
    public class UserList : List
    {
        public string Username { get; set; }
        public string UserGroupName { get; set; }
        public string Level { get; set; }
    }
}
