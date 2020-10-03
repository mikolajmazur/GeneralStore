using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Config
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string ExpirationInDays { get; set; }
    }
}
