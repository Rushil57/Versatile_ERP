using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class StockTransferDestinationDetailsViewModel
    {
        public int Id { get; set; }
        public string StockTransferId { get; set; }
        public int InventoryId { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string SerialIds { get; set; }
        public string SerialNos { get; set; }

        public string InventoryName { get; set; }
        public SelectList InventorySelectList { get; set; }
    }
}
