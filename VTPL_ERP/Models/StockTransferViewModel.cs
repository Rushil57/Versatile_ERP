using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class StockTransferViewModel
    {
        public string StockJournalNo { get; set; }
        public DateTime StockTransferDate { get; set; }
        public string StockTransferDateString { get; set; }
        public string StockTransferDateDisplay { get; set; }
        public List<InventoryMasterDto> StockItemList { get; set; }
        public string SourceBranchName { get; set; }
        public SelectList SourceBranchSelectList { get; set; }
        public SelectList DestinationBranchSelectList { get; set; }
        public SelectList SourceInventorySelectList { get; set; }

        public StockTransferSourceViewModel StockTransferSourceObj { get; set; }
        public StockTransferDestinationViewModel StockTransferDestinationObj { get; set; }
        public List<StockTransferSourceDetailsViewModel> StockTransferSourceDetails { get; set; }
        public List<StockTransferDestinationDetailsViewModel> StockTransferDestinationDetails { get; set; }

        public StockTransferSourceDto StockTransferSourceData { get; set; }
        public StockTransferDestinationDto StockTransferDestinationData { get; set; }
        public List<StockTransferSourceDetailsDto> StockTransferSourceDetailsData { get; set; }
        public List<StockTransferDestinationDetailsDto> StockTransferDestinationDetailsData { get; set; }
    }
}
