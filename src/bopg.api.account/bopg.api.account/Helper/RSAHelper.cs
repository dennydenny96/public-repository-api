using bopg.api.account.Output;
using System;
using System.Security.Cryptography;

namespace bopg.api.account.Helper
{
    public class RSAHelper
    {
        public static RSAPublicKey GetPublicKey(RSAParameters param)
        {
            var retVal = new RSAPublicKey()
            {
                Modulus = Convert.ToBase64String(param.Modulus),
                Exponent = Convert.ToBase64String(param.Exponent)
            };

            return retVal;
        }

        public static RSAPrivateKey GetPrivateKey(RSAParameters param)
        {
            var retVal = new RSAPrivateKey()
            {
                Modulus = Convert.ToBase64String(param.Modulus),
                Exponent = Convert.ToBase64String(param.Exponent),
                P = Convert.ToBase64String(param.P),
                Q = Convert.ToBase64String(param.Q),
                DP = Convert.ToBase64String(param.DP),
                DQ = Convert.ToBase64String(param.DQ),
                InverseQ = Convert.ToBase64String(param.InverseQ),
                D = Convert.ToBase64String(param.D),
            };

            return retVal;
        }
    }
}
