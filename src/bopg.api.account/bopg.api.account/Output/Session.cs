using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class Session : OutputBase
    {
        public SessionContent Content { get; set; }

        public Session()
        {
            this.Content = new SessionContent();
        }
    }

    public class SessionContent
    {
        public List<SessionData> Data { get; set; }

        public SessionContent()
        {
            this.Data = new List<SessionData>();
        }
    }

    public class SessionData
    {
        public string CompanyID { get; set; }
        public string OperatorID { get; set; }
        public string WebLevel { get; set; }
        public string UserGuid { get; set; }
        public Int32 UserID { get; set; }
        public string UserGroup { get; set; }
        public string isFirstLogin { get; set; }
        public string StatusDeveloper { get; set; }
        public string AuthKey { get; set; }
        public string SessionToken { get; set; }
        public string UserLogin { get; set; }
    }
}
