using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WDCM_Api.Model
{
    public class LoginModel
    {
        public string Password { get; set; }
        public string Phone { get; set; }
        public string SerialDevice { get; set; }
        public bool IsPaid { get; set; }
    }
}
