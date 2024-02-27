using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class InventoryCategoryMasterDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
