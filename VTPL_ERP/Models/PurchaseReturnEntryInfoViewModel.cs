using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PurchaseReturnEntryInfoViewModel
    {
        public PurchaseReturnEntryDto PurchaseReturnEntryData { get; set; }
        public CompanyDto CompanyData { get; set; }
        public List<PurchaseReturnEntryDetailsDto> PurchaseReturnEntryDetails { get; set; }
        public List<PurchaseReturnEntryOtherChargesDetailsDto> PurchaseReturnEntryOtherChargesDetails { get; set; }
        public string TotalAmountinWord { get; set; }
    }
}
