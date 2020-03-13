using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopg.api.account.Output
{
    public class RSA : OutputBase
    {
        public RSAPrivateKey PrivateKey { get; set; }
        public RSAPublicKey PublicKey { get; set; }
    }

    public class RSAPublicKey
    {
        public string Modulus { get; set; }
        public string Exponent { get; set; }
    }

    public class RSAPrivateKey : RSAPublicKey
    {
        public string P { get; set; }
        public string Q { get; set; }
        public string DP { get; set; }
        public string DQ { get; set; }
        public string InverseQ { get; set; }
        public string D { get; set; }
    }
}
