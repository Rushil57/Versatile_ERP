using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockItemDetailsDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int BranchId { get; set; }
        public int UnitId { get; set; }
        public int AvailableQty { get; set; }
        public int FYId { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Created_by { get; set; }
        public int Modified_by { get; set; }
    }
}
