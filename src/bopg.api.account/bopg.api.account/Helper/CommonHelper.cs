using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace bopg.api.account.Helper
{
    public class CommonHelper
    {
        public static Output.RSA CreateRSAKeyPair()
        {
            var retVal = new BaseHelper();
            var objJSON = new Output.RSA();

            try
            {
                var rsa = RSA.Create(2048);

                objJSON.PublicKey = RSAHelper.GetPublicKey(rsa.ExportParameters(false));
                objJSON.PrivateKey = RSAHelper.GetPrivateKey(rsa.ExportParameters(true));

                //objJSON.PrivateKey = Convert.ToBase64String(rsa.Key.Export(CngKeyBlobFormat.GenericPrivateBlob));
                //objJSON.PublicKey = Convert.ToBase64String(rsa.Key.Export(CngKeyBlobFormat.GenericPublicBlob));

                objJSON.ResultCode = 1;
                objJSON.ErrorMessage = "";
            }
            catch (Exception ex)
            {
                retVal.Exception = ex;

                if (ex is System.Data.SqlClient.SqlException sqlEx)
                {
                    retVal.SQLInfo($"sp:{sqlEx.Procedure}, line:{sqlEx.LineNumber}");
                    retVal.SQLException = true;

                    objJSON.ResultCode = 69998;
                    objJSON.ErrorMessage = "SQL Exception";
                }
                else
                {
                    objJSON.ResultCode = 69999;
                    objJSON.ErrorMessage = "Unknown Error";
                }
            }
            finally
            {
                retVal.SerializeObject<Output.OutputBase>(objJSON);
            }

            return objJSON;
        }

    }
}
