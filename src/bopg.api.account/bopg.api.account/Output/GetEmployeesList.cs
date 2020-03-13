using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class GetEmployeesList : OutputBase
    {
        public GetEmployeesListContent Content { get; set; }

        public GetEmployeesList()
        {
            this.Content = new GetEmployeesListContent();
        }
    }

    public class GetEmployeesListContent
    {
        public Int32 TotalRows { get; set; }
        public List<GetEmployeesListData> Data { get; set; }

        public GetEmployeesListContent()
        {
            this.Data = new List<GetEmployeesListData>();
        }
    }

    public class GetEmployeesListData
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Int32 JobTitleID {get; set; }
        public DateTime HireDate { get; set; }
    }
}
