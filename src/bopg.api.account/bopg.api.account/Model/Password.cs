using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopg.api.account.Model
{
    public class Password : Session
    {
        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
