using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockTransferSourceDto
    {
        public string Id { get; set; }
        public DateTime StockTransferDate { get; set; }
        public int BranchId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string Narration { get; set; }
    }
}
