using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PurchaseQuotationDetailsViewModel
    {
        public int Id { get; set; }
        public string PQNo { get; set; }
        public int InventoryId { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal GrossAmt { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscAmt { get; set; }
        //public int UnitId { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalAmount { get; set; }
        public string SerialNos { get; set; }
        public decimal CentralTax { get; set; }
        public decimal StateTax { get; set; }
        public string HSNCode { get; set; }
    }
}
