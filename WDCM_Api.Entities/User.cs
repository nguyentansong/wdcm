using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WDCM_Api.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        public string SerialDevice { get; set; }
        public bool? IsPaid { get; set; }

        public int? Role { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
