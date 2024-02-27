using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IInventoryGSTDetailsService
    {
        Task<int> InsertUpdateInventoryGSTDetails(InventoryGSTDetailsDto model);

        Task DeleteInventoryGSTDetailsByKey(int id);
        Task DeleteInventoryGSTDetailsByInventoryId(int id);

        Task<InventoryGSTDetailsDto> GetInventoryGSTDetailsByKey(int id);

        Task<List<InventoryGSTDetailsDto>> GetAllInventoryGSTDetails();

        Task<InventoryGSTDetailsDto> GetInventoryGSTDetailsByInventoryId(int InventoryId);
    }
}
