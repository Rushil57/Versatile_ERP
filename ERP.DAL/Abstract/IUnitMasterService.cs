using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IUnitMasterService
    {
        Task<int> InsertUpdateUnit(UnitMasterDto model);

        Task DeleteUnitByKey(int id);

        Task<UnitMasterDto> GetUnitByKey(int id);

        Task<List<UnitMasterDto>> GetAllUnit();

    }
}
