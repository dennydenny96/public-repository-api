using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace bopg.api.account.Entities
{
    public class Logger
    {
        #region -= Fields =-
        private readonly string server_url = string.Empty;
        private readonly Int32 server_port = 12201;
        #endregion

        #region -= Properties =-
        public GelfMessage GelfMessage { get; set; }
        #endregion

        #region -= Constructor =-
        public Logger(string loggerName)
        {
            this.GelfMessage = new GelfMessage();

            this.GelfMessage.LoggerName = loggerName;
            this.GelfMessage.Host = Dns.GetHostName();
            this.GelfMessage.App = Program.Configuration.GetSection("GrayLog:App").Value;
            this.GelfMessage.Facility = Program.Configuration.GetSection("GrayLog:Facility").Value;
            this.server_url = Program.Configuration.GetSection("GrayLog:ServerURL").Value;
            this.server_port = Int32.Parse(Program.Configuration.GetSection("GrayLog:Port").Value);
        }
        #endregion

        #region -= Methods =-
        public void SetInfo()
        {
            this.GelfMessage.Level = 1;

            var message = CreateJSON(this.GelfMessage);
            Send(message);
        }

        public void SetWarn()
        {
            this.GelfMessage.Level = 2;

            var message = CreateJSON(this.GelfMessage);
            Send(message);
        }

        public void SetError()
        {
            this.GelfMessage.Level = 3;

            var message = CreateJSON(this.GelfMessage);
            Send(message);
        }

        #region -= Private =-
        public static string CreateJSON(GelfMessage gelfMsg)
        {
            var json = new Newtonsoft.Json.Linq.JObject();

            // Base Gelf
            json.Add("version", "1.1");
            json.Add("host", gelfMsg.Host);
            json.Add("short_message", gelfMsg.ShortMessage);
            json.Add("full_message", gelfMsg.FullMessage);
            json.Add("level", gelfMsg.Level);

            // Custom
            json.Add("_facility", gelfMsg.Facility);
            json.Add("_app", gelfMsg.App);
            json.Add("_logger_name", gelfMsg.LoggerName);
            json.Add("_server_ip", gelfMsg.ServerIP);
            json.Add("_remote_ip", gelfMsg.RemoteIP);
            json.Add("_url", gelfMsg.URL);
            if (gelfMsg.ErrorCode != 0)
            {
                json.Add("_error_code", gelfMsg.ErrorCode);
                json.Add("_error_message", gelfMsg.ErrorMessage);
            }
            json.Add("_status", gelfMsg.Status);
            if (!string.IsNullOrEmpty(gelfMsg.RawJSONInput))
                json.Add("_raw_json_input", gelfMsg.RawJSONInput);
            json.Add("_elapsed_time", gelfMsg.ElapsedTime);
            json.Add("_output", gelfMsg.Output);
            if (!string.IsNullOrEmpty(gelfMsg.SQLDetail))
                json.Add("_sql_detail", gelfMsg.SQLDetail);
            if (!string.IsNullOrEmpty(gelfMsg.ProcedureFlow))
                json.Add("_procedure_flow", gelfMsg.ProcedureFlow);
            if (!string.IsNullOrEmpty(gelfMsg.TrackingCode))
                json.Add("_tracking_code", gelfMsg.TrackingCode);
            if (!string.IsNullOrEmpty(gelfMsg.StampUser))
                json.Add("_stamp_user", gelfMsg.StampUser);
            json.Add("_sql_exception", (gelfMsg.SQLException) ? "true" : "false");
            if (gelfMsg.SQLElapsed > 0)
                json.Add("_sql_elapsed_time", gelfMsg.SQLElapsed);

            // Additional Fields
            if (gelfMsg.ListFields != null && gelfMsg.ListFields.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var field in gelfMsg.ListFields)
                {
                    json.Add("_" + field.Key, field.Value);
                }

            }

            // Exception
            if (gelfMsg.Exception != null)
            {
                json.Add("_exception_message", gelfMsg.ExceptionMessage);
                json.Add("_exception_source", gelfMsg.ExceptionSource);
                json.Add("_exception_stack_trace", gelfMsg.ExceptionStackTrace);
            }

            // Convert to JSON String
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;

            var body = JsonConvert.SerializeObject(json, jsonSettings);

            return body;
        }

        private bool Send(string message)
        {
            bool retVal = UDPSender(message);

            return retVal;
        }
        private bool UDPSender(string message)
        {
            try
            {
                using (var udpClient = new UdpClient())
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    return udpClient.SendAsync(bytes, bytes.Length, this.server_url, this.server_port).Result > 0;
                }
            }
            catch
            {
                return false;
            }

        }
        #endregion
        #endregion
    }

    public class GelfMessage
    {
        #region -= Fields =-
        private const Int32 shortMsgLength = 250;
        private const Int32 fullMsgLength = 1024;
        private const Int32 msgLength = 31 * 1024;
        private string shortMsg;
        private string fullMsg;
        private string outputMsg;
        private Exception exception;
        #endregion

        #region -= Properties =-
        // Base Gelf
        public string Host { get; set; }
        public string ShortMessage
        {
            get { return shortMsg; }
            set
            {
                if (value.Length > shortMsgLength)
                {
                    shortMsg = value.Substring(0, shortMsgLength - 1);
                }
                else
                    shortMsg = value;
            }
        }
        public string FullMessage
        {
            get
            {
                if (string.IsNullOrEmpty(fullMsg))
                    return string.Empty;
                else
                    return fullMsg;
            }
            set
            {
                if (value.Length > fullMsgLength)
                {
                    fullMsg = value.Substring(0, fullMsgLength - 1);
                }
                else
                    fullMsg = value;
            }
        }
        public Int32 Level { get; set; }

        // Exception
        public Exception Exception
        {
            get
            {
                return exception;
            }
            set
            {
                exception = value;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }
        }
        public string ExceptionMessage
        {
            get
            {
                if (exception != null)
                    return exception.Message;
                else
                    return null;
            }
        }

        public string ExceptionStackTrace
        {
            get
            {
                if (exception != null)
                    return exception.StackTrace;
                else
                    return null;
            }
        }

        public string ExceptionSource
        {
            get
            {
                if (exception != null)
                    return exception.Source;
                else
                    return null;
            }
        }

        // Custom
        public string App { get; set; }
        public string Facility { get; set; }
        public string LoggerName { get; set; }
        public string ServerIP { get; set; }
        public string RemoteIP { get; set; }
        public string URL { get; set; }
        public Int32 ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Status
        {
            get
            {
                if (Level == 1)
                    return "success";
                else
                    return "failed";
            }
        }
        public string RawJSONInput { get; set; }
        public double ElapsedTime { get; set; }
        public string Output
        {
            get
            {
                if (string.IsNullOrEmpty(outputMsg))
                    return string.Empty;
                else
                    return outputMsg;
            }
            set
            {
                if (value.Length > msgLength)
                {
                    outputMsg = value.Substring(0, msgLength - 1);
                }
                else
                    outputMsg = value;
            }
        }
        public string SQLDetail { get; set; }
        public string ProcedureFlow { get; set; }
        public string TrackingCode { get; set; }
        public string StampUser { get; set; }
        public bool SQLException { get; set; }
        public double SQLElapsed { get; set; }

        // Additonal Fields
        public Dictionary<string, string> ListFields { get; set; }
        #endregion
    }
}
