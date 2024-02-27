using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PEOtherChargesDetailsDto
    {
        public int Id { get; set; }
        public int OtherChargeId { get; set; }
        public Decimal Amount { get; set; }
        public string PENo { get; set; }
        public string OtherChargeName { get; set; }
        public Decimal FinalAmount { get; set; }
        public string ChargeHSNCode { get; set; }
        public decimal ChargeIGST { get; set; }
    }
}
