using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class InventoryMasterDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public int UnitId { get; set; }
       // public int BranchId { get; set; }
        public string TypesOfGood { get; set; }
        public string ServiceType { get; set; }
        public decimal Rate { get; set; }
        public bool IsGSTApplicable { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
        public InventoryGSTDetailsDto GSTDetail { get; set; }
        public int AvailableStock { get; set; }
        public string UnitName { get; set; }
    }
}
