using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IRolesMasterService
    {
        Task<int> InsertUpdateRole(RolesMasterDto model);

        Task DeleteRoleByKey(int id);

        Task<RolesMasterDto> GetRoleByKey(int id);

        Task<List<RolesMasterDto>> GetAllRole();
    }
}
