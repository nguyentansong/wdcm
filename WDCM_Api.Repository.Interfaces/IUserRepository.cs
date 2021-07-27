using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Entities;

namespace WDCM_Api.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task Create(User User);
        User AuthenticateByPhone(string phone, string password);
        Task<bool> IsExistPhone(string phone);
        Task<bool> IsExistSerial(string serial);
    }
}
