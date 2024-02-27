using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IInventoryMasterService
    {
        Task<int> InsertUpdateInventory(InventoryMasterDto model);

        Task DeleteInventoryByKey(int id);

        Task<InventoryMasterDto> GetInventoryByKey(int id);

        Task<List<InventoryMasterDto>> GetAllInventory();
        Task<List<InventoryMasterDto>> GetAllAccountPartyWithAvailableStock(int branchId);
    }
}
