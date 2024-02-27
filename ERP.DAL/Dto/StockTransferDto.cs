using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.DAL.Dto
{
    public class StockTransferDto
    {
        public string StockJournalNo { get; set; }
        public DateTime StockTransferDate { get; set; }
        public string StockTransferDateString { get; set; }
        public string StockTransferDateDisplay { get; set; }

        public List<InventoryMasterDto> StockItemList { get; set; }
        public string SourceBranchName { get; set; }

        public StockTransferSourceDto StockTransferSourceData { get; set; }
        public StockTransferDestinationDto StockTransferDestinationData { get; set; }
        public List<StockTransferSourceDetailsDto> StockTransferSourceDetails { get; set; }
        public List<StockTransferDestinationDetailsDto> StockTransferDestinationDetails { get; set; }
    }
}
