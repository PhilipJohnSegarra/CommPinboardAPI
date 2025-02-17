using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Dtos
{
    public class LogInRequest
    {
        public string username {set; get; }
        public string password {set; get; }
    }
}