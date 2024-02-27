using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class SalesOrderViewModel
    {
        public string SONo { get; set; }
        public string SalesPersonId { get; set; }
        public DateTime SalesDate { get; set; }
        public string SalesDateString { get; set; }
        public string SODateDisplay { get; set; }
        public int SupplierId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<SalesOrderDetailsDto> SalesOrderDetailsData { get; set; }
        public string PageMode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierGSTNumber { get; set; }
        public string SupplierPANNumber { get; set; }
        public string SupplierStateTINNo { get; set; }
        public string SupplierStateName { get; set; }
        public int BranchId { get; set; }

        public List<AccountPartyMasterDto> SupplierList { get; set; }
        public List<InventoryMasterDto> StockItemList { get; set; }
        public List<SalesOrderDetailsViewModel> SalesOrderDetails { get; set; }

        public SelectList SupplierSelectList { get; set; }
        public SelectList StockItemSelectList { get; set; }
        public SelectList SalesPersonSelectList { get; set; }
        public SelectList BranchSelectList { get; set; }

        public int SelectedBranchId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedStockItemId { get; set; }
        public string SelectedSalesPersonId { get; set; }
    }
}
