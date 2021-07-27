using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Data;
using WDCM_Api.Entities;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Repository
{
    public class MenuRepository : BaseRepository, IMenuRepository
    {
        public MenuRepository(WDCMDbContext context) :base (context)
        {
        }

        public async Task<List<Menu>> GetMenu()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<List<Menu>> GetMenuByParentId(int Id)
        {
            return await _context.Menus.Where(x => x.ParentId == Id).ToListAsync();
        }

        public async Task<Menu> GetMenyById(int id)
        {
            return await _context.Menus.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
