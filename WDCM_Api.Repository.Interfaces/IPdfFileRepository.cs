using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Entities;

namespace WDCM_Api.Repository.Interfaces
{
    public interface IPdfFileRepository :IBaseRepository
    {
        Task AddPdfFile(PdfFiles pdfFiles);

        Task<List<PdfFiles>> GetListPdfByParentId(int? parentId,int page);

        Task<PdfFiles> GetPdfFileById(Guid PdfFileId);

        Task<bool> DeletedFileById(Guid Id);
    }
}
