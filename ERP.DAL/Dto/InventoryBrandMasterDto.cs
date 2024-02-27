using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class InventoryBrandMasterDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
