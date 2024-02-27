using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IOutwardService
    {
        Task<int> InsertUpdateOutward(OutwardDto model);

        Task DeleteOutwardByKey(int outwardId);

        Task<OutwardDto> GetOutwardByKey(int outwardId);

        Task<List<OutwardDto>> GetAllOutward();
    }
}
