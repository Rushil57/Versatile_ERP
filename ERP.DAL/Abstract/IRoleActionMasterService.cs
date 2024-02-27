using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IRoleActionMasterService
    {
        Task<int> InsertUpdateRoleAction(RoleActionMasterDto model);

        Task DeleteRoleActionByKey(int id);

        Task<RoleActionMasterDto> GetRoleActionByKey(int id);

        Task<List<RoleActionMasterDto>> GetAllRoleAction();
    }
}
