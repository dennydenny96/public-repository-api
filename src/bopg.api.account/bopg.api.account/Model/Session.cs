using System;

namespace bopg.api.account.Model
{
    public class Session : BaseModel
    {
        public string UserLogin { get; set; }
        public string SessionToken { get; set; }
        public string AuthorizationID { get; set; }
        public Int32? UserID { get; set; }
    }
}
