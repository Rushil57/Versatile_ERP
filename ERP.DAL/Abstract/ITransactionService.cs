using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface ITransactionService
    {
        Task InsertUpdateStockMovement(StockMovementDto model);
        Task<int> InsertStockMovementSerialDetails(StockMovementSerialDetailsDto model);
        Task<int> InsertUpdateStockItemDetails(StockItemDetailsDto model);
        Task DeleteStockItemDetailsByInventoryId(int inventoryId);
        Task DeleteStockMovementByInventoryId(int inventoryId);
        Task<int> GetAvailableStockItemDetailsByInventoryId(int inventoryId);
        Task DeleteStockMovementByTranCode(string tranCode);
    }
}
