using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class UserSignInDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
