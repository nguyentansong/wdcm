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
    public class PdfFileRepository : BaseRepository, IPdfFileRepository
    {
        public PdfFileRepository(WDCMDbContext context) :base(context) { }
        public async Task AddPdfFile(PdfFiles pdfFiles)
        {
            await _context.AddAsync(pdfFiles);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletedFileById(Guid Id)
        {
            PdfFiles pdfFiles = _context.PdfFiles.SingleOrDefault(x => x.Id == Id);
            if (pdfFiles != null)
            {
                _context.Remove(pdfFiles);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PdfFiles>> GetListPdfByParentId(int? parentId,int page)
        {
            int take = 20;
            int skip = (page -1) * take;
            return await _context.PdfFiles.Where(x => x.ParentId == parentId).Skip(skip).Take(take).OrderByDescending(x=>x.CreateDate).ToListAsync();
        }

        public async Task<PdfFiles> GetPdfFileById(Guid PdfFileId)
        {
            var data = _context.PdfFiles.SingleOrDefault(x => x.Id == PdfFileId);
            return data;
        }
    }
}
