using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PEInfoViewModel
    {
        public PurchaseEntryDto PurchaseEntryData { get; set; }
        public CompanyDto CompanyData { get; set; }
        public List<PurchaseEntryDetailsDto> PurchaseEntryDetails { get; set; }
        public List<PEOtherChargesDetailsDto> PEOtherChargesDetails { get; set; }
        public string TotalAmountinWord { get; set; }
    }
}
