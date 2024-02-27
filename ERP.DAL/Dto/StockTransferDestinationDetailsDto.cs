using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockTransferDestinationDetailsDto
    {
        public int Id { get; set; }
        public string StockTransferId { get; set; }
        public int InventoryId { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string SerialIds { get; set; }
        public string SerialNos { get; set; }
    }
}
