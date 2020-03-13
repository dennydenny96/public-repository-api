using System.Data.SqlClient;

namespace bopg.api.account.Entities
{
    public class Dashboard
    {
        public static BasicEntity MenuGet(Model.User data, Output.Menu obj)
        {
            var retVal = new BasicEntity();

            retVal.AddParameter("@user_id", data.UserID);
            data.SqlDetail = retVal.SQLCommandBuilder("spDashboardMenuGet");

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
                        var itemSection = new Output.SectionData();
                        var itemMenu = new Output.MenuData();

                        itemMenu.MenuID = (reader.IsDBNull(0)) ? string.Empty : reader.GetString(0);
                        itemMenu.MenuName = (reader.IsDBNull(1)) ? string.Empty : reader.GetString(1);
                        itemMenu.MenuOrder = (reader.IsDBNull(2)) ? 0 : reader.GetInt32(2);
                        itemMenu.WebName = (reader.IsDBNull(3)) ? string.Empty : reader.GetString(3);
                        itemMenu.WebURL = (reader.IsDBNull(4)) ? string.Empty : reader.GetString(4);

                        itemSection.SectionID = (reader.IsDBNull(5)) ? string.Empty : reader.GetString(5);
                        itemSection.SectionName = (reader.IsDBNull(6)) ? string.Empty : reader.GetString(6);
                        itemSection.SectionIcon = (reader.IsDBNull(7)) ? string.Empty : reader.GetString(7);

                        var objFind = obj.Content.Data.Find(d => d.SectionID == itemSection.SectionID);
                        if (objFind is null)
                        {
                            itemSection.Data.Add(itemMenu);
                            obj.Content.Data.Add(itemSection);
                        }
                        else
                        {
                            objFind.Data.Add(itemMenu);
                        }

                    }
                }

                reader.Close();
            }

            retVal.Close();

            return retVal;
        }
    }
}
