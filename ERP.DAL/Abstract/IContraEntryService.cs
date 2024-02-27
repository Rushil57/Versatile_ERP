using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;
namespace ERP.DAL.Abstract
{
    public interface IContraEntryService
    {
        Task<string> InsertUpdateContraEntry(ContraEntryDto model);

        Task DeleteContraEntryByKey(string id);

        Task<ContraEntryDto> GetContraEntryByKey(string id);

        Task<List<ContraEntryDto>> GetAllContraEntry();
    }
}
