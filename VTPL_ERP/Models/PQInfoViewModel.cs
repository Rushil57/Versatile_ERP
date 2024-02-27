using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PQInfoViewModel
    {
        public PurchaseQuotationDto PurchaseQuotationData { get; set; }
        public List<PurchaseQuotationDetailsDto> PurchaseQuotationDetails { get; set; }
        public List<PQOtherChargesDetailsDto> PQOtherChargesDetails { get; set; }
    }
}
