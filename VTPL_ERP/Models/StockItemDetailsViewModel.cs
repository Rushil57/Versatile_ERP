using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class StockItemDetailsViewModel
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
