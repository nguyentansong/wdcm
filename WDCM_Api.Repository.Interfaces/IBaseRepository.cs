using System;
using System.Threading.Tasks;

namespace WDCM_Api.Repository.Interfaces
{
    public interface IBaseRepository
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
