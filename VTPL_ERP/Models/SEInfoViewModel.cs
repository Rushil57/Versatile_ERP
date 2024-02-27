using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class SEInfoViewModel
    {
        public SalesEntryDto SalesEntryData { get; set; }
        public CompanyDto CompanyData { get; set; }
        public List<SalesEntryDetailsDto> SalesEntryDetails { get; set; }
        public List<SEOtherChargesDetailsDto> SEOtherChargesDetails { get; set; }
        public string TotalAmountinWord { get; set; }
    }
}
