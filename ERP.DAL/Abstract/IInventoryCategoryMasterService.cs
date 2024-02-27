using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IInventoryCategoryMasterService
    {
        Task<int> InsertUpdateInventoryCategory(InventoryCategoryMasterDto model);

        Task DeleteInventoryCategoryByKey(int id);

        Task<InventoryCategoryMasterDto> GetInventoryCategoryByKey(int id);

        Task<List<InventoryCategoryMasterDto>> GetAllInventoryCategory();
    }
}
