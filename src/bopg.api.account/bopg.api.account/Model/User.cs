using System;

namespace bopg.api.account.Model
{
    public class User : Session
    {
        public string UserLoginAdd { get; set; }
        public string UserNameAdd { get; set; }
        public string EmailAdd { get; set; }
        public string PasswordAdd { get; set; }
        public string LevelAdd { get; set; }
        public string CompanyAdd { get; set; }
        public string OperatorAdd { get; set; }
    }
}
