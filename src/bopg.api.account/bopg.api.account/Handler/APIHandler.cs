using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace bopg.api.account.Handler
{
    public class APIHandler
    {
        #region -= Fields =-
        private readonly IHttpClientFactory ClientFactory;
        #endregion

        #region -= Constructor =-
        public APIHandler(RequestDelegate requestDelegate, IHttpClientFactory httpClientFactory)
        {
            this.ClientFactory = httpClientFactory;
        }
        #endregion

        #region -= Methods =-
        public async Task Invoke(HttpContext context)
        {
            string response = GenerateResponse(context);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }

        private string GenerateResponse(HttpContext context)
        {
            string retVal = string.Empty;
            string path = context.Request.Path;

            switch (path.ToLower())
            {
                case "/registration.api": { retVal = new Services.Registration(context).GetResponse(); break; }
                case "/create/account/login/user.api": { retVal = new Services.CreateAccountLoginUser(context).GetResponse(); break; }
                case "/login.api": { retVal = new Services.Login(context).GetResponse(); break; }
                //case "/validate_session.api": { retVal = new Services.ValidateSession(context).GetResponse(); break; }
                case "/logout.api": { retVal = new Services.Logout(context).GetResponse(); break; }

                case "/dashboard/menu.api": { retVal = new Services.Menu(context).GetResponse(); break; }

                case "/page/user.api": { retVal = new Services.User(context).GetResponse(); break; }
                case "/page/user_add.api": { retVal = new Services.UserAdd(context).GetResponse(); break; }
                case "/page/user_delete.api": { retVal = new Services.UserDelete(context).GetResponse(); break; }
                case "/page/user_change_password.api": { retVal = new Services.ChangePassword(context).GetResponse(); break; }

                //case "/get/user.api": { retVal = new Services.GetUserList(context).GetResponse(); break; }

                //Menu
                case "/menu/list.api": { retVal = new Services.MenuList(context).GetResponse(); break; }
                
                //Employee
                case "/get/employees/list.api": { retVal = new Services.GetEmployeesList(context).GetResponse(); break; }
                case "/employee/add.api": { retVal = new Services.EmployeeAdd(context).GetResponse(); break; }
                case "/employee/edit.api": { retVal = new Services.EmployeeEdit(context).GetResponse(); break; }
                case "/employee/delete.api": { retVal = new Services.EmployeeDelete(context).GetResponse(); break; }
                
                //Combo                
                case "/combo/departments.api": { retVal = new Services.ComboDepartmentName(context).GetResponse(); break; }
                case "/combo/jobtitles.api": { retVal = new Services.ComboJobTitle(context).GetResponse(); break; }

                //User
                case "/userlist.api": { retVal = new Services.UserList(context).GetResponse(); break; }
                case "/useradd.api": { retVal = new Services.UserLoginAdd(context).GetResponse(); break; }

                //Master Loan Book
                case "/master/loan/book/list.api": { retVal = new Services.MasterLoanBookList(context).GetResponse(); break; }
                case "/master/loan/book/add.api": { retVal = new Services.MasterLoanBookAdd(context).GetResponse(); break; }

                //Transaction Loan Book
                case "/transaction/loan/book/list.api": { retVal = new Services.TransactionsLoanBookList(context).GetResponse(); break; }
                case "/transaction/loan/book/borrow.api": { retVal = new Services.TransactionLoanBookBorrow(context).GetResponse(); break; }
                case "/transaction/loan/book/return.api": { retVal = new Services.TransactionLoanBookReturn(context).GetResponse(); break; }

                //Report Loan Book
                case "/report/loan/book/list.api": { retVal = new Services.ReportLoanBookList(context).GetResponse(); break; }

                case "/combo/databook.api": { retVal = new Services.ComboDataBook(context).GetResponse(); break; }
                
                default: { retVal = DefaultResponse(); break; }

            }

            return retVal;
        }

        private string DefaultResponse()
        {
            var response = new { ResultCode = 69999, ErrorMessage = "Path Not Found" };

            return JsonConvert.SerializeObject(response);
        }
        #endregion
    }
}
