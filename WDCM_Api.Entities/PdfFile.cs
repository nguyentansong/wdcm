using System;
using System.Collections.Generic;
using System.Text;

namespace WDCM_Api.Entities
{
    public class PdfFiles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PdfFile { get; set; }
        public string FullUrlFile { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
