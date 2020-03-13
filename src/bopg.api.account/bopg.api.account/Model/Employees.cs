using System;

namespace bopg.api.account.Model
{
    public class Employees : Session
    {
        public Int64 ID { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Int32 JobTitleID { get; set; }
        public DateTime HireDate { get; set; }

    }
}
