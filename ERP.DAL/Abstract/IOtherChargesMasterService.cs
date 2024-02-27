using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IOtherChargesMasterService
    {
        Task<int> InsertUpdateOtherCharges(OtherChargesMasterDto model);

        Task DeleteOtherChargesByKey(int id);

        Task<OtherChargesMasterDto> GetOtherChargesByKey(int id);

        Task<List<OtherChargesMasterDto>> GetAllOtherCharges();
    }
}
