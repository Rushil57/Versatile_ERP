using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IInwardService
    {
        Task<int> InsertUpdateInward(InwardDto model);

        Task DeleteInwardByKey(int inwardId);

        Task<InwardDto> GetInwardByKey(int inwardId);

        Task<List<InwardDto>> GetAllInward();
    }
}
