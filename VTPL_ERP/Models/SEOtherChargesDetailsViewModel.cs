using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class SEOtherChargesDetailsViewModel
    {
        public int Id { get; set; }
        public int OtherChargeId { get; set; }
        public Decimal Amount { get; set; }
        public string SalesEntryNo { get; set; }
        public string OtherChargeName { get; set; }
        public Decimal FinalAmount { get; set; }
        public string ChargeHSNCode { get; set; }
        public decimal ChargeIGST { get; set; }
    }
}
