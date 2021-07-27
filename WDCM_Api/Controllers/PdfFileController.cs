using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfFileController : ControllerBase
    {
        public readonly IPdfFileRepository _pdfFileRepository;
        public PdfFileController(IPdfFileRepository pdfFileRepository)
        {
            _pdfFileRepository = pdfFileRepository;
        }

        [HttpPost()]
        public async Task<ActionResult> AddFilePdf(PdfFiles pdfFiles)
        {
            await _pdfFileRepository.AddPdfFile(pdfFiles);
            return Ok(new ApiResponse(true));
        }

        [HttpGet("listpdf")]
        public async Task<ActionResult> GetListPdf([FromQuery] int? parentId, int page = 1)
        {
            var data = await _pdfFileRepository.GetListPdfByParentId(parentId, page);
            return Ok(data);
        }

        [HttpGet("pdf")]
        public async Task<ActionResult> GetPdfFileById([FromQuery] Guid pdfFileId)
        {
            var data = await _pdfFileRepository.GetPdfFileById(pdfFileId);
            return Ok(data);
        }

        [HttpDelete("")]
        public async Task<ActionResult> DeleteFileById([FromQuery] Guid id)
        {
            try
            {
                bool IsDeletedSuccess = await _pdfFileRepository.DeletedFileById(id);
                if (IsDeletedSuccess)
                {
                    return Ok(new ApiResponse(true));
                }
                else
                {
                    return BadRequest(new ApiResponse(false, null, "Không thể xoá file. Vui lòng thử lại", -1));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse(false, null, ex.Message, -2));
            }
            
        }
    }
}
