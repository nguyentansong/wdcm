using System;
using System.Collections.Generic;
using System.Text;

namespace WDCM_Api.Entities.Response
{
    public class AuthenticateReponse
    {
        public string AccessToken { get; set; }
        public int ExpireIn { get; set; }

        public Guid Id { get; set; }
        public string Phone { get; set; }

        public string Password { get; set; }

        public int? Role { get; set; }

        public bool? IsPaid { get; set; }

        public DateTime? CreateDate { get; set; }

    }
}
