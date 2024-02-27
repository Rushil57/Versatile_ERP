using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PurchaseQuotationViewModel
    {
        public string PQNo { get; set; }
        public DateTime PQDate { get; set; }
        public string PQDateString { get; set; }
        public string PQDateDisplay { get; set; }
        public List<BranchMasterDto> BranchList { get; set; }
        public List<AccountPartyMasterDto> SupplierList { get; set; }
        public List<InventoryMasterDto> StockItemList { get; set; }
        public List<OtherChargesMasterDto> OtherChargesList { get; set; }
        public List<PurchaseQuotationDetailsViewModel> PurchaseQuotationDetails { get; set; }
        public List<PQOtherChargesDetailsViewModel> PQOtherChargesDetails { get; set; }
        

        public SelectList BranchSelectList { get; set; }
        public SelectList SupplierSelectList { get; set; }
        public SelectList StockItemSelectList { get; set; }
        public SelectList OtherChargesSelectList { get; set; }

        public int SelectedBranchId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedStockItemId { get; set; }
        public int SelectedOtherChargesId { get; set; }

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
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string PageMode { get; set; }
        public string BranchName { get; set; }
        public string SupplierName { get; set; }
        public string JsonOtherChargesData { get; set; }
    }
}
