using System;
using System.Collections.Generic;
using System.Text;

namespace WDCM_Api.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsHasSub { get; set; }
    }
}
