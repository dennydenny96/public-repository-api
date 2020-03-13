using System.Data.SqlClient;
using System;
namespace bopg.api.account.Entities
{
    public class Combo
    {
        public static BasicEntity ComboDepartmentName(Model.List data, Output.Combo obj)
        {
            var retVal = new BasicEntity();

            data.SqlDetail = retVal.SQLCommandBuilder("spComboDepartment");

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
                        var item = new Output.ComboData();

                        item.ID = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                        item.Name = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity ComboJobTitle(Model.ComboJobTitle data, Output.Combo obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@id", data.ID);

            data.SqlDetail = retVal.SQLCommandBuilder("spComboJobTitle");

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
                        var item = new Output.ComboData();

                        item.ID = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                        item.Name = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

        public static BasicEntity ComboDataBook(Model.List data, Output.ComboDataBook obj)
        {
            var retVal = new BasicEntity();

            data.SqlDetail = retVal.SQLCommandBuilder("spComboDataBook");

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
                        var item = new Output.ComboDataBookData();

                        item.ID = (reader.IsDBNull(0)) ? 0 : reader.GetInt32(0);
                        item.Title = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        item.Price = (reader.IsDBNull(2)) ? string.Empty : reader.GetString(2);
                        item.Borrowed = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);

                        obj.Content.Data.Add(item);
                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }

    }
}
