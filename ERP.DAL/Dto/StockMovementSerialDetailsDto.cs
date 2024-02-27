using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockMovementSerialDetailsDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int BranchId { get; set; }
        public int FYId { get; set; }
        public string TranBook { get; set; }
        public string TranType { get; set; }
        public string TranId { get; set; }
        public int TranDetailId { get; set; }
        public int Qty { get; set; }
        public string SerailNo { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int StockMovementId { get; set; }
    }
}
