using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IPhoneCallsEntryService
    {
        Task<int> InsertUpdatePhoneCallsEntry(PhoneCallsEntryDto model);

        Task DeletePhoneCallsEntryByKey(int id);

        Task<PhoneCallsEntryDto> GetPhoneCallsEntryByKey(int id);

        Task<List<PhoneCallsEntryDto>> GetAllPhoneCallsEntry();

    }
}
