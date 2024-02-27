using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PurchaseEntryDto
    {
        public string PENo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public int SupplierId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string PurchaseAccount { get; set; }
        public string Narration { get; set; }
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
        public int BranchId { get; set; }
        public List<PurchaseEntryDetailsDto> PurchaseEntryDetails { get; set; }
        public List<PEOtherChargesDetailsDto> PEOtherChargesDetails { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierGSTNumber { get; set; }
        public string SupplierPANNumber { get; set; }
        public string SupplierStateTINNo { get; set; }
        public string SupplierStateName { get; set; }
        public string BranchName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
