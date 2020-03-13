using Newtonsoft.Json;
using System;
using System.Text;

namespace bopg.api.account.Doc
{
    public class DocAllAPI
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

        public static string Write()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("===========================================================================");
            sb.AppendLine("List All API");
            sb.AppendLine("===========================================================================");

            sb.AppendLine(User());

            return sb.ToString();
        }

        public static string User()
        {

                StringBuilder sb = new StringBuilder();
                Int16 number = 0;
                number = 0;

                sb.AppendLine("---------------------------------------------------------------------------");
                sb.AppendLine("User Configuration");
                sb.AppendLine("---------------------------------------------------------------------------");
                sb.AppendLine(++number + ") /page/user_change_password.api");
                sb.AppendLine("example-request :");
                var passwd = new Model.Password { UserID = 1, OldPassword = "aaaa", NewPassword = "aaaaa" };
                sb.AppendLine(JsonConvert.SerializeObject(passwd, settings));
                sb.AppendLine();
                sb.AppendLine(++number + ") /page/user_delete.api");
                sb.AppendLine("example-request :");
                var del = new Model.User { UserID = 1, AuthorizationID="" };
                sb.AppendLine(JsonConvert.SerializeObject(del, settings));
                sb.AppendLine();

            return sb.ToString();
        }


    }
}
