using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Entities;

namespace WDCM_Api.Repository.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenu();
        Task<List<Menu>> GetMenuByParentId(int parentId);
        Task<Menu> GetMenyById(int id);
    }
}
