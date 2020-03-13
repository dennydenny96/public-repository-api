using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class GetUserList : OutputBase
    {
        public GetUserListContent Content { get; set; }

        public GetUserList()
        {
            this.Content = new GetUserListContent();
        }
    }

    public class GetUserListContent
    {
        public Int32 TotalRows { get; set; }
        public List<GetUserListData> Data { get; set; }

        public GetUserListContent()
        {
            this.Data = new List<GetUserListData>();
        }
    }

    public class GetUserListData
    {
        public string Name { get; set; }
        public Int32 Age { get; set; }
        public string City { get; set; }
    }
}
