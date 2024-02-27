using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PurchaseReturnEntryDetailsDto
    {
        public int Id { get; set; }
        public string DebitNoteNo { get; set; }
        public int InventoryId { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal GrossAmt { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscAmt { get; set; }
        public int UnitId { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalAmount { get; set; }
        public string SerialNos { get; set; }
        public string InventoryItemName { get; set; }
        public decimal CentralTax { get; set; }
        public decimal StateTax { get; set; }
        public string HSNCode { get; set; }
        public decimal GrossAmountData { get; set; }
        public string SerialIds { get; set; }
        public string Unit { get; set; }
    }
}
