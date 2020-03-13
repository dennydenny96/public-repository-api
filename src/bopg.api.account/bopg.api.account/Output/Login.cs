using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class Login : OutputBase
    {
        public LoginContent Content { get; set; }

        public Login()
        {
            this.Content = new LoginContent();
        }
    }

    public class LoginContent
    {
        public List<LoginData> Data { get; set; }

        public LoginContent()
        {
            this.Data = new List<LoginData>();
        }
    }

    public class LoginData
    {
        public string UserID { get; set; }
        public string SessionToken { get; set; }
        public string Level { get; set; }
    }
}
