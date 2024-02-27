using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface ICourierMasterService
    {
        Task<int> InsertUpdateCourier(CourierMasterDto model);

        Task DeleteCourierByKey(int id);

        Task<CourierMasterDto> GetCourierByKey(int id);

        Task<List<CourierMasterDto>> GetAllCourier();
    }
}
