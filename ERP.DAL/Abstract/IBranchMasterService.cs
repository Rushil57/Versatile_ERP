using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IBranchMasterService
    {
        Task<int> InsertUpdateBranch(BranchMasterDto model);

        Task DeleteBranchByKey(int id);

        Task<BranchMasterDto> GetBranchByKey(int id);

        Task<List<BranchMasterDto>> GetAllBranch();
    }
}
