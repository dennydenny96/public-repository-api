using System;

namespace bopg.api.account.Output
{
    public class OutputBase
    {
        public Int32 ResultCode { get; set; }
        public string ErrorMessage { get; set; }

        public OutputBase()
        {
            this.ResultCode = 0;
            this.ErrorMessage = "Default Error Message";
        }

        public OutputBase(Exception ex)
        {
            this.ResultCode = 69999;
            this.ErrorMessage = ex.Message;
        }
    }
}
