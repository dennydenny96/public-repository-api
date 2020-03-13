using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class GetEmployees
    {
        public static BasicEntity GetEmployeesList(Model.GetEmployeesList data, Output.GetEmployeesList obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@id", data.ID);
            retVal.AddParameter("@NIK", data.NIK);
            data.SqlDetail = retVal.SQLCommandBuilder("spEmployeeList");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                if (retVal.ResultCode == 1)
                {
                    reader.NextResult();
                    while (reader.Read())
                    {
                        var item = new Output.GetEmployeesListData();

                        item.NIK = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        item.FirstName = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.LastName = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.Gender = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);
                        item.Address = (reader.IsDBNull(4)) ? string.Empty : reader.GetString(4);
                        item.PlaceOfBirth = (reader.IsDBNull(5)) ? string.Empty : reader.GetString(5);
                        item.DateOfBirth = (reader.IsDBNull(6)) ? DateTime.MinValue : reader.GetDateTime(6);
                        item.Email = (reader.IsDBNull(7)) ? string.Empty : reader.GetString(7);
                        item.Phone = (reader.IsDBNull(8)) ? string.Empty : reader.GetString(8);
                        item.JobTitleID = (reader.IsDBNull(9)) ? 0 : reader.GetInt32(9);
                        item.HireDate = (reader.IsDBNull(10)) ? DateTime.MinValue : reader.GetDateTime(10);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity EmployeeAdd(Model.Employees data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@NIK", data.NIK);
            retVal.AddParameter("@FirstName", data.FirstName);
            retVal.AddParameter("@LastName", data.LastName);
            retVal.AddParameter("@Address", data.Address);
            retVal.AddParameter("@Gender", data.Gender);
            retVal.AddParameter("@PlaceOfBirth", data.PlaceOfBirth);
            retVal.AddParameter("@DateOfBirth", data.DateOfBirth);
            retVal.AddParameter("@Email", data.Email);
            retVal.AddParameter("@Phone", data.Phone);
            retVal.AddParameter("@JobTitleID", data.JobTitleID);
            retVal.AddParameter("@HireDate", data.HireDate);

            data.SqlDetail = retVal.SQLCommandBuilder("spAddEmployee");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity EmployeeEdit(Model.Employees data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@ID", data.ID);
            retVal.AddParameter("@NIK", data.NIK);
            retVal.AddParameter("@FirstName", data.FirstName);
            retVal.AddParameter("@LastName", data.LastName);
            retVal.AddParameter("@Address", data.Address);
            retVal.AddParameter("@Gender", data.Gender);
            retVal.AddParameter("@PlaceOfBirth", data.PlaceOfBirth);
            retVal.AddParameter("@DateOfBirth", data.DateOfBirth);
            retVal.AddParameter("@Email", data.Email);
            retVal.AddParameter("@Phone", data.Phone);
            retVal.AddParameter("@JobTitleID", data.JobTitleID);
            retVal.AddParameter("@HireDate", data.HireDate);

            data.SqlDetail = retVal.SQLCommandBuilder("spEditEmployee");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity EmployeeDelete(Model.Employees data, Output.OutputBase obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@id", data.ID);
            retVal.AddParameter("@nik", data.NIK);
            
            data.SqlDetail = retVal.SQLCommandBuilder("spDeleteEmployee");

            using (SqlDataReader reader = retVal.ExecReader())
            {
                while (reader.Read())
                {
                    obj.ResultCode = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                    obj.ErrorMessage = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                    retVal.ResultCode = obj.ResultCode;
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

    }
}
