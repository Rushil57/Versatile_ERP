using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IProblemMasterService
    {
        Task<int> InsertUpdateProblem(ProblemMasterDto model);

        Task DeleteProblemByKey(int id);

        Task<ProblemMasterDto> GetProblemByKey(int id);

        Task<List<ProblemMasterDto>> GetAllProblem();
    }
}
