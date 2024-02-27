using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PurchaseQuotationDto
    {
        public string PQNo { get; set; }
        public DateTime PQDate { get; set; }
        public int BranchId { get; set; }
        public int SupplierId { get; set; }
        public int TotalQty { get; set; }
        public decimal Amount { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal RoundOff { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public List<PurchaseQuotationDetailsDto> PurchaseQuotationDetails { get; set; }
        public List<PQOtherChargesDetailsDto> PQOtherChargesDetails { get; set; }
        public string BranchName { get; set; }
        public string SupplierName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
