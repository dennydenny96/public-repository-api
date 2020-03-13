using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using bopg.api.account.Entities;

namespace bopg.api.account.Services
{
    public abstract class BaseService
    {
        #region -= Fields =-
        private Logger logger;
        private DateTime timeStart;
        private DateTime timeStop;
        #endregion

        #region -= Properties =-
        protected readonly HttpContext Context;
        protected readonly string JSONStringInput;
        protected GelfMessage GrayLogMessage { get { return this.logger.GelfMessage; } }
        protected readonly bool IsJSONStringInputEmpty;
        #endregion

        #region -= Constructor =-
        public BaseService(HttpContext httpContext)
        {
            timeStart = DateTime.Now;
            Context = httpContext;
            JSONStringInput = new StreamReader(this.Context.Request.Body).ReadToEnd();
            IsJSONStringInputEmpty = (string.IsNullOrEmpty(JSONStringInput)) ? true : false;
        }
        #endregion

        #region -= Methods =-
        public abstract string GetResponse();

        private void SetInitGrayLogMessage()
        {
            this.GrayLogMessage.RawJSONInput = JSONStringInput;
            this.GrayLogMessage.ServerIP = this.Context.Connection.LocalIpAddress.ToString();
            this.GrayLogMessage.RemoteIP = this.Context.Connection.RemoteIpAddress.ToString();
            this.GrayLogMessage.URL = string.Format("{0}://{1}{2}", this.Context.Request.Scheme, this.Context.Request.Host, this.Context.Request.Path);
        }

        #region -= Error =-
        protected string SetErrorJSONStringEmpty()
        {
            var err = new { ResultCode = 40000, ErrorMessage = "Unknown Error", Result = "" };
            var errJSON = JsonConvert.SerializeObject(err, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            GrayLogMessage.ErrorCode = err.ResultCode;
            GrayLogMessage.ErrorMessage = err.ErrorMessage;

            return errJSON;
        }

        protected string SetErrorUnknownError(Exception ex)
        {
            var err = new { ResultCode = 40000, ErrorMessage = "Unknown Error", Result = "" };

            GrayLogMessage.ErrorCode = err.ResultCode;
            GrayLogMessage.ErrorMessage = err.ErrorMessage;
            GrayLogMessage.Exception = ex;

            return JsonConvert.SerializeObject(err, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        private void SetErrorBL(Int32 errorCode, string errorMessage)
        {
            this.GrayLogMessage.ErrorCode = errorCode;
            this.GrayLogMessage.ErrorMessage = errorMessage;
        }

        private void SetErrorBL(Int32 errorCode, string errorMessage, Exception ex)
        {
            this.GrayLogMessage.ErrorCode = errorCode;
            this.GrayLogMessage.ErrorMessage = errorMessage;
            this.GrayLogMessage.Exception = ex;
        }
        #endregion

        protected Tuple<bool, string, T> ParsingJSONStringInput<T>()
        {
            try
            {
                var model = JsonConvert.DeserializeObject<T>(this.JSONStringInput);

                Type objTracking = model.GetType();
                object tracking = objTracking.GetProperty("TrackingCode").GetValue(model);

                if (tracking != null)
                    this.GrayLogMessage.TrackingCode = tracking.ToString();

                Type objStampUser = model.GetType();
                object stampUser = objStampUser.GetProperty("StampUser").GetValue(model);

                if (stampUser != null)
                    this.GrayLogMessage.StampUser = stampUser.ToString();

                return Tuple.Create<bool, string, T>(true, string.Empty, model);
            }
            catch (Exception ex)
            {
                var err = new { ResultCode = 40000, ErrorMessage = "Unknown Error", Result = "" };
                var errJSON = JsonConvert.SerializeObject(err, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                GrayLogMessage.ErrorCode = err.ResultCode;
                GrayLogMessage.ErrorMessage = err.ErrorMessage;
                GrayLogMessage.Exception = ex;

                return Tuple.Create<bool, string, T>(false, errJSON, default(T));
            }
        }

        protected void SetGrayLogNewFields(string key, string value)
        {
            if (this.GrayLogMessage.ListFields == null)
                this.GrayLogMessage.ListFields = new Dictionary<string, string>();

            this.GrayLogMessage.ListFields.Add(key, value);
        }

        protected string ProcessResult(Helper.BaseHelper helper)
        {
            string retVal = helper.JSONString;

            this.GrayLogMessage.Output = retVal;
            this.GrayLogMessage.SQLDetail = helper.SQLDetail;
            this.GrayLogMessage.ProcedureFlow = helper.ProcedureFlow;
            this.GrayLogMessage.StampUser = helper.StampUser;
            this.GrayLogMessage.SQLException = helper.SQLException;
            this.GrayLogMessage.SQLElapsed = helper.SQLElapsed;

            if (helper.IsError)
            {
                if (helper.IsThrowException)
                {
                    this.SetErrorBL(helper.ResultCode, helper.ErrorMessage, helper.Exception);
                    this.SetLoggerError();
                }
                else
                {
                    this.SetErrorBL(helper.ResultCode, helper.ErrorMessage);
                    this.SetLoggerWarn();
                }
            }
            else
            {
                this.GrayLogMessage.FullMessage = retVal;
                this.SetLoggerInfo();
            }

            return retVal;
        }

        #region -= Logger =-
        protected void SetLoggerName(string name)
        {
            this.logger = new Logger(name);
            SetInitGrayLogMessage();
        }

        protected void SetLoggerInfo()
        {
            if (this.logger != null)
            {
                timeStop = DateTime.Now;
                this.GrayLogMessage.ElapsedTime = timeStop.Subtract(timeStart).TotalSeconds;
                this.logger.SetInfo();
            }
        }

        protected void SetLoggerWarn()
        {
            if (this.logger != null)
            {
                timeStop = DateTime.Now;
                this.GrayLogMessage.ElapsedTime = timeStop.Subtract(timeStart).TotalSeconds;
                this.logger.SetWarn();
            }
        }

        protected void SetLoggerError()
        {
            if (this.logger != null)
            {
                timeStop = DateTime.Now;
                this.GrayLogMessage.ElapsedTime = timeStop.Subtract(timeStart).TotalSeconds;
                this.logger.SetError();
            }
        }
        #endregion
        #endregion
    }
}
