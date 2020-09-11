using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class UserLogin
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
