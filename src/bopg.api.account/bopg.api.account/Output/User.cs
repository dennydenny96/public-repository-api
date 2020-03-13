using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class User : OutputBase
    {
        public UserContent Content { get; set; }

        public User()
        {
            this.Content = new UserContent();
        }
    }

    public class UserContent
    {
        public Int32 TotalRows { get; set; }
        public List<UserData> Data { get; set; }

        public UserContent()
        {
            this.Data = new List<UserData>();
        }
    }

    public class UserData
    {
        public Int32 UserID { get; set; }
        public string UserGroupName { get; set; }
        public string UserLogin { get; set; }
        public string UserActive { get; set; }
        public string UserSuspend { get; set; }
    }
}
