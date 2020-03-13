using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class MenuList : OutputBase
    {
        public MenuListContent Content { get; set; }

        public MenuList()
        {
            this.Content = new MenuListContent();
        }
    }

    public class MenuListContent
    {
        public Int32 TotalRows { get; set; }
        public List<MenuListData> Data { get; set; }

        public MenuListContent()
        {
            this.Data = new List<MenuListData>();
        }
    }

    public class MenuListData
    {
        public Int64 ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitleName { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth{ get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string NIK { get; set; }
        public Int32 DepartmentID { get; set; }
        public Int32 JobTitleID { get; set; }
    }
}
