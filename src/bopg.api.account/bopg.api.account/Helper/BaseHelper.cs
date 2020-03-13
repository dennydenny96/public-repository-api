using Newtonsoft.Json;
using System;
using System.Text;

namespace bopg.api.account.Helper
{
    public class BaseHelper
    {
        #region -= Fields =-
        private Exception exception;
        private StringBuilder sb = new StringBuilder();
        private Int32 stepNumber = 0;
        private StringBuilder sbSql = new StringBuilder();
        #endregion

        #region -= Properties =-
        public Int32 ResultCode { get; set; }
        public string ErrorMessage { get; set; }
        public string JSONString { get; set; }
        public bool IsThrowException
        {
            get
            {
                if (exception == null)
                    return false;
                else
                    return true;
            }
        }
        public Exception Exception { get { return exception; } set { exception = value; this.ResultCode = 69999; this.ErrorMessage = exception.Message; } }
        public bool IsError { get; set; }
        public string SQLDetail { get { return sbSql.ToString(); } }
        public string ProcedureFlow { get { return sb.ToString(); } }
        public string StampUser { get; set; }
        public bool SQLException { get; set; }
        public double SQLElapsed { get; set; }
        #endregion

        #region -= Constructor =-
        public BaseHelper()
        {
            this.IsError = true;
            this.SQLException = false;
        }
        #endregion

        #region -= Methods =-
        public void SerializeObject<T>(T data)
        {
            JSONString = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        public void Step(string label, int resultCode)
        {

            var resultValue = (resultCode == 1) ? "Success[1]" : "Error[" + ResultCode + "]";
            sb.AppendLine(string.Format("{0}) {1} : [{2}] result = {3}", ++stepNumber, "PaymentGateway", label, resultValue));
        }

        public void SQLInfo(string sql)
        {
            sbSql.AppendLine(sql);
        }
        #endregion
    }
}
