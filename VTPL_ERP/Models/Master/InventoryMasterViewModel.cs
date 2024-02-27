using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models.Master
{
    public class InventoryMasterViewModel
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public int UnitId { get; set; }
        //public int BranchId { get; set; }
        public string TypesOfGood { get; set; }
        public string ServiceType { get; set; }
        public decimal Rate { get; set; }
        public bool IsGSTApplicable { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }

        //public string HSNCode { get; set; }
        //public string CalculationType { get; set; }
        //public string Taxability { get; set; }
        //public decimal IntegratedTax { get; set; }
        //public decimal CentralTax { get; set; }
        //public decimal StateTax { get; set; }
        public InventoryGSTDetailsViewModel GSTDetail { get; set; }
        public List<InventoryBrandMasterViewModel> BrandList { get; set; }
        public List<InventoryCategoryMasterViewModel> CategoryList { get; set; }
        public List<UnitMasterViewModel> UnitList { get; set; }
        public List<BranchMasterViewModel> BranchList { get; set; }

        public int AvailableStock { get; set; }
        public string UnitName { get; set; }
    }
}
