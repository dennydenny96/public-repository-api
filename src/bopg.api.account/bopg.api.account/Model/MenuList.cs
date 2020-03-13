using System;

namespace bopg.api.account.Model
{
    public class MenuList : List
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Int32 DepartmentID { get; set; }
        public Int32 JobTitleID { get; set; }

    }
}
