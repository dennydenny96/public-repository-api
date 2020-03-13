using System;

namespace bopg.api.account.Model
{
    public class UserLoginAdd : Session
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        
    }
}
