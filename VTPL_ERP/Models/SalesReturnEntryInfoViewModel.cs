using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class SalesReturnEntryInfoViewModel
    {
        public SalesReturnEntryDto SalesReturnEntryData { get; set; }
        public CompanyDto CompanyData { get; set; }
        public List<SalesReturnEntryDetailsDto> SalesReturnEntryDetails { get; set; }
        public List<SalesReturnEntryOtherChargesDetailsDto> SalesReturnEntryOtherChargesDetails { get; set; }
        public string TotalAmountinWord { get; set; }
    }
}
