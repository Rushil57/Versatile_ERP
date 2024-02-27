using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;

namespace ERP.DAL.Abstract
{
    public interface IStockTransferService
    {
        Task<List<StockTransferSourceDto>> GetAllStockTransferSource();
        Task<List<StockTransferDestinationDto>> GetAllStockTransferDestination();
        Task<string> InsertUpdateStockTransferSource(StockTransferSourceDto model);
        Task<int> InsertUpdateStockTransferSourceDetails(StockTransferSourceDetailsDto model);
        Task<string> InsertUpdateStockTransferDestination(StockTransferDestinationDto model);
        Task<int> InsertUpdateStockTransferDestinationDetails(StockTransferDestinationDetailsDto model);
        Task InsertStockTransferSourceItemSerialNosDetails(string stockTransferId, int inventoryId, string itemSerialNo);
        Task InsertStockTransferDestinationItemSerialNosDetails(string stockTransferId, int inventoryId, string itemSerialNo);
        Task DeleteStockTransferSourceByKey(int id);
        Task DeleteStockTransferSourceDetailsBySourceId(int sourceId);
        Task DeleteStockTransferDestinationByKey(int id);
        Task DeleteStockTransferDestinationDetailsByDestiantionId(int destId);
        Task<StockTransferSourceDto> GetStockTransferSourceByKey(int id);
        Task<List<StockTransferSourceDetailsDto>> GetStockTransferSourceDetailsBySourceId(int sourceId);
        Task<StockTransferDestinationDto> GetStockTransferDestinationByKey(int id);
        Task<List<StockTransferDestinationDetailsDto>> GetStockTransferDestinationDetailsByDestinationId(int destId);
    }
}
