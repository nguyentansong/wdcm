using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Data;
using WDCM_Api.Entities;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public readonly WDCMDbContext _context;
        public BaseRepository(WDCMDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
