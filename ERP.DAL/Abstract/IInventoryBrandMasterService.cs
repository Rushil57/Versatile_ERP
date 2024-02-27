using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IInventoryBrandMasterService
    {
        Task<int> InsertUpdateInventoryBrand(InventoryBrandMasterDto model);

        Task DeleteInventoryBrandByKey(int id);

        Task<InventoryBrandMasterDto> GetInventoryBrandByKey(int id);

        Task<List<InventoryBrandMasterDto>> GetAllInventoryBrand();
    }
}
