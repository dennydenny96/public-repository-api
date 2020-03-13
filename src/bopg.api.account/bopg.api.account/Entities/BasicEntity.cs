using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace bopg.api.account.Entities
{
    public class BasicEntity
    {
        #region -= Fields =-
        private List<SqlParameter> Parameters = new List<SqlParameter>();
        private DateTime timeStart;
        private DateTime timeStop;
        private string storedProcedureName;
        #endregion

        #region -= Properties =-
        public Int32 ResultCode { get; set; }
        public string SQLDetail { get; set; }
        public static string ConnectionString
        {
            get
            {
                return Program.Configuration.GetSection("ConnectionStrings:Service").Value;
            }
        }
        public Int32 Total { get; set; }
        public Int32 SqlTimeOut { get; set; }
        public double SQLElapsed { get { return (timeStop != null) ? timeStop.Subtract(timeStart).TotalSeconds : 0; } }
        #endregion

        #region -= Constructor =-
        public BasicEntity()
        {
            this.ResultCode = 0;
            this.SqlTimeOut = 0;
            this.timeStart = DateTime.Now;
        }
        #endregion

        #region -= Methods =-
        public string SQLCommandBuilder(string spName)
        {
            StringBuilder sb = new StringBuilder();
            this.storedProcedureName = spName;
            var dbName = string.Empty;

            using (var sqlServerConn = new SqlConnection(ConnectionString))
            {
                dbName = sqlServerConn.Database;
                sqlServerConn.Close();
            }

            if (this.Parameters.Count > 0)
            {
                sb.Append($"EXEC {dbName}.dbo.{spName} ");
                var first = true;

                foreach (var p in Parameters)
                {
                    if (first)
                    {
                        first = false;

                        if ((p.DbType == System.Data.DbType.String) || (p.DbType == System.Data.DbType.DateTime))
                            sb.AppendFormat("{0}='{1}'", p.ParameterName, (p.Value is null) ? "NULL" : p.Value);
                        else
                            sb.AppendFormat("{0}={1}", p.ParameterName, (p.Value is null) ? "NULL" : p.Value);
                    }
                    else
                    {
                        if ((p.DbType == System.Data.DbType.String) || (p.DbType == System.Data.DbType.DateTime))
                            sb.AppendFormat(", {0}='{1}'", p.ParameterName, (p.Value is null) ? "NULL" : p.Value);
                        else
                            sb.AppendFormat(", {0}={1}", p.ParameterName, (p.Value is null) ? "NULL" : p.Value);
                    }
                }
            }
            else
            {
                sb.Append($"EXEC {dbName}.dbo.{spName} ");
            }

            this.SQLDetail = sb.ToString();

            return this.SQLDetail;
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            this.Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }

        public SqlDataReader ExecReader()
        {
            string sqlConnectionString = string.Empty;
            if (SqlTimeOut > 0)
                sqlConnectionString = BasicEntity.ConnectionString + ";Connection Timeout=" + SqlTimeOut.ToString();
            else
                sqlConnectionString = BasicEntity.ConnectionString;

            if (this.Parameters.Count > 0)
            {
                return SqlHelper.ExecuteReader(sqlConnectionString, CommandType.StoredProcedure, this.storedProcedureName, Parameters.ToArray());
            }
            else
            {
                return SqlHelper.ExecuteReader(sqlConnectionString, CommandType.StoredProcedure, this.storedProcedureName);
            }
        }

        public void ExecNonQuery()
        {
            string sqlConnectionString = string.Empty;
            if (SqlTimeOut > 0)
                sqlConnectionString = BasicEntity.ConnectionString + ";Connection Timeout=" + SqlTimeOut.ToString();
            else
                sqlConnectionString = BasicEntity.ConnectionString;

            if (Parameters.Count > 0)
                SqlHelper.ExecuteNonQuery(sqlConnectionString, CommandType.StoredProcedure, this.storedProcedureName, Parameters.ToArray());
            else
                SqlHelper.ExecuteNonQuery(sqlConnectionString, CommandType.StoredProcedure, this.storedProcedureName);

        }

        public void Close()
        {
            timeStop = DateTime.Now;
        }
        #endregion
    }
}
