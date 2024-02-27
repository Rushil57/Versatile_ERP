using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockTransferDestinationDto
    {
        public int Id { get; set; }
        public DateTime StockTransferDate { get; set; }
        public string StockTransferId { get; set; }
        public int BranchId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string Narration { get; set; }
    }
}
