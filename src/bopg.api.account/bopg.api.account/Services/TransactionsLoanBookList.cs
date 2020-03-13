using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;

namespace bopg.api.account.Services
{
    public class TransactionsLoanBookList : BaseService
    {
        public TransactionsLoanBookList(HttpContext httpContext) : base(httpContext)
        {
        }

        public override string GetResponse()
        {
            string retVal = string.Empty;
            var methodType = MethodBase.GetCurrentMethod().DeclaringType;
            this.SetLoggerName(methodType.FullName);
            this.GrayLogMessage.ShortMessage = methodType.Name;

            try
            {
                if (this.IsJSONStringInputEmpty)
                {
                    retVal = this.SetErrorJSONStringEmpty();
                    this.SetLoggerWarn();
                }
                else
                {
                    var tuple = this.ParsingJSONStringInput<Model.TransactionsLoanBookList>();
                    if (tuple.Item1)
                    {
                        var data = tuple.Item3;

                        retVal = this.ProcessResult(Helper.TransactionsLoanBookHelper.TransactionsLoanBookList(data));
                    }
                    else
                    {
                        retVal = tuple.Item2;
                        this.SetLoggerError();
                    }

                }
            }
            catch (Exception ex)
            {
                retVal = this.SetErrorUnknownError(ex);
                this.SetLoggerError();
            }

            return retVal;
        }
    }
}
