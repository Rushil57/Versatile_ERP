using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ERP.DAL.Abstract
{
    public interface IRolesAndRightsMasterService
    {
        Task<int> InsertUpdateRolesAndRights(RolesAndRightsMasterDto model);
        Task<List<RolesAndRightsMasterDto>> GetRolesAndRights(int RoleId, int PageId);
        Task DeleteRolesAndRightsByKeys(int RoleId, int PageId);
        Task<List<RolesAndRightsMasterDto>> GetRolesAndRightsByRoleId(int RoleId);
    }
}
