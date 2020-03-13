using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class UserList : OutputBase
    {
        public UserListContent Content { get; set; }

        public UserList()
        {
            this.Content = new UserListContent();
        }
    }

    public class UserListContent
    {
        public Int32 TotalRows { get; set; }
        public List<UserListData> Data { get; set; }

        public UserListContent()
        {
            this.Data = new List<UserListData>();
        }
    }

    public class UserListData
    {
        public string UserLogin { get; set; }
        public string Level { get; set; }
    }
}
